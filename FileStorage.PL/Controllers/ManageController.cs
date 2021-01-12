using AutoMapper;
using FileStorage.BLL.DTO;
using FileStorage.BLL.Interfaces;
using FileStorage.PL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FileStorage.PL.Controllers
{
    /// <summary>
    /// ManageController, manage user profile, accsess only authorize users
    /// </summary>
    [Authorize]
    public class ManageController : Controller
    {
        private const long MAX_STORAGE_SIZE = 5368709120; // 5GB

        private readonly IFileService fileService;

        private readonly ISharedFileService sharedService;

        private readonly IStorageService storageService;

        private readonly IUserService userService;

        private readonly IMapper mapper;

        public ManageController(IFileService _fileService, IStorageService _storageService,
            ISharedFileService _sharedService, IUserService _userService, IMapper _mapper)
        {
            fileService = _fileService;
            sharedService = _sharedService;
            storageService = _storageService;
            userService = _userService;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var userName = User.Identity.Name;

            await storageService.CreateStorage(userName);
            ViewBag.StorageSize = await storageService.GetStorageSize(await storageService.GetStorageIdByUserName(userName));
            ViewBag.MaxStorageSize = MAX_STORAGE_SIZE;
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> ViewUsers()
        {
            ViewBag.StorageSize = await storageService.GetStorageSize(await storageService.GetStorageIdByUserName(User.Identity.Name));
            var users = mapper.Map<List<StorageModel>>(await storageService.GetAllStorages());
            long total_size = 0;
            foreach (var item in users)
            {
                total_size += item.StorageSize;
            }
            ViewBag.TotalSize = total_size;
            return View(users);
        }

        [HttpGet]
        public async Task<ActionResult> ManageSharedFiles()
        {
            var sharedFiles = await sharedService.GetByAllByUserName(User.Identity.Name);
            return View(mapper.Map<List<SharedViewModel>>(sharedFiles));
        }

        [HttpPost]
        public async Task<ActionResult> CancelSharing(string link)
        {
            await sharedService.Delete(User.Identity.Name, link);
            return RedirectToAction("ManageSharedFiles");
        }

        [HttpPost]
        public async Task<ActionResult> CancelAllSharing()
        {
            await sharedService.DeleteAll(User.Identity.Name);
            return RedirectToAction("ManageSharedFiles");
        }

        [HttpGet]
        public async Task<ActionResult> Trash()
        {
            var filesDtos = await fileService.GetTrashFiles(await storageService.GetStorageIdByUserName(User.Identity.Name));
            return View(mapper.Map<List<FileViewModel>>(filesDtos));
        }

        [HttpPost]
        public async Task<ActionResult> RemoveFileFromTrash(string name)
        {
            int storageId = await storageService.GetStorageIdByUserName(User.Identity.Name);
            var file = await fileService.GetFileByName(name, storageId);
            System.IO.File.Delete(Server.MapPath(file.Path));
            await fileService.RemoveFileFromTrash(storageId, name);
            await storageService.ReduceStorageSize(storageId, file.Length);
            return RedirectToAction("Trash");
        }

        [HttpPost]
        public async Task<ActionResult> RestoreFileFromTrash(string name)
        {
            int storageId = await storageService.GetStorageIdByUserName(User.Identity.Name);
            await fileService.RestoreFileFromTrash(storageId, name);
            return RedirectToAction("Trash");
        }

        [HttpPost]
        public async Task<ActionResult> RemoveAllFromTrash()
        {
            int storageId = await storageService.GetStorageIdByUserName(User.Identity.Name);
            var files = await fileService.GetTrashFiles(storageId);
            foreach (var item in files)
            {
                await storageService.ReduceStorageSize(storageId, item.Length);
                await fileService.RemoveFileFromTrash(storageId, item.Name);
            }
            return RedirectToAction("Trash");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel model)
        {
            UserDto userDto = new UserDto { Email = User.Identity.Name, Password = model.OldPassword };

            if (await userService.ChangePassword(userDto, model.NewPassword))
            {
                ViewBag.Message = "Password changed.";
                return View();
            }
            else
            {
                ViewBag.Message = "Invalid input.";
                return View(model);
            }
        }
    }
}