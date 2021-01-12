using AutoMapper;
using FileStorage.BLL.DTO;
using FileStorage.BLL.Interfaces;
using FileStorage.PL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FileStorage.PL.Controllers
{
    /// <summary>
    /// HomeController, view user files and control them, accsess only authorize users
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        private const long MAX_STORAGE_SIZE = 5368709120; // 5GB

        private readonly IFileService fileService;

        private readonly ISharedFileService sharedService;

        private readonly IStorageService storageService;

        private readonly IMapper mapper;

        public HomeController(IFileService _fileService, IStorageService _storageService,
            ISharedFileService _sharedService, IMapper _mapper)
        {
            fileService = _fileService;
            sharedService = _sharedService;
            storageService = _storageService;
            mapper = _mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Index(int page = 1, string sort_type = "6")
        {
            int pageSize = 20;
            ViewBag.CurrentSort = sort_type;
            int storageId = await storageService.GetStorageIdByUserName(User.Identity.Name);

            var filesDto = await fileService.GetStorageFiles(storageId);
            var filesViewModel = mapper.Map<List<FileViewModel>>(filesDto);

            switch (sort_type)
            {
                case "1":
                    filesViewModel = filesViewModel.OrderBy(x => x.Name).ToList();
                    break;

                case "2":
                    filesViewModel = filesViewModel.OrderByDescending(x => x.Name).ToList();
                    break;

                case "3":
                    filesViewModel = filesViewModel.OrderBy(x => x.Length).ToList();
                    break;

                case "4":
                    filesViewModel = filesViewModel.OrderByDescending(x => x.Length).ToList();
                    break;

                case "5":
                    filesViewModel = filesViewModel.OrderBy(x => x.CreationDate).ToList();
                    break;

                case "6":
                    filesViewModel = filesViewModel.OrderByDescending(x => x.CreationDate).ToList();
                    break;

                default:
                    filesViewModel = filesViewModel.OrderByDescending(x => x.CreationDate).ToList();
                    break;
            }

            var filesPerPages = filesViewModel.Skip((page - 1) * pageSize).Take(pageSize);
            var pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = filesViewModel.Count
            };
            var resultModel = new IndexPageViewModel
            {
                PageInfo = pageInfo,
                Files = filesPerPages
            };
            return View(resultModel);
        }

        [HttpPost]
        public async Task<ActionResult> Index(string select_sort_type)
        {
            return await Index(sort_type: select_sort_type);
        }

        [HttpPost]
        public async Task<ActionResult> Upload(HttpPostedFileBase[] uploads)
        {
            int added_files = 0;
            int exist_files = 0;
            string userName = User.Identity.Name;

            await storageService.CreateStorage(userName);
            int storageId = await storageService.GetStorageIdByUserName(userName);

            if (uploads == null || uploads[0] == null || uploads.Length > 10)
            {
                TempData["ValidationMessage"] = "Choose from 1 to 10 files";
            }
            else
            {
                int total_size = 0;
                foreach (var upload in uploads)
                    total_size += upload.ContentLength;
                if (total_size >= 524288000)
                {
                    TempData["ValidationMessage"] = "Total size should not exceed 500MB";
                }
                else
                {
                    foreach (var upload in uploads)
                    {
                        var storageSize = await storageService.GetStorageSize(storageId);
                        if (storageSize + upload.ContentLength > MAX_STORAGE_SIZE)
                        {
                            TempData["ValidationMessage"] = "Storage size reached the limit in 5GB";
                            break;
                        }

                        string fileName = Path.GetFileName(upload.FileName);
                        int length = upload.ContentLength;
                        string type = upload.ContentType;
                        string path = "~/Files/" + Guid.NewGuid();

                        if (!await fileService.IsFileExist(fileName, storageId))
                        {
                            FileDto fileModel = new FileDto()
                            {
                                Name = fileName,
                                Path = path,
                                Length = length,
                                StorageId = storageId,
                                Type = type
                            };

                            await fileService.AddFile(fileModel);
                            await storageService.IncreaseStorageSize(storageId, length);
                            upload.SaveAs(Server.MapPath(path));
                            added_files++;
                        }
                        else exist_files++;
                    }

                    TempData["UploadInfo"] = added_files > 0 ? $"Added {added_files} new file(s)." : "";
                    TempData["UploadInfo"] += exist_files > 0 ? $" {exist_files} file(s) already exist." : "";
                }
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> GetFile(string name)
        {
            int storageId = await storageService.GetStorageIdByUserName(User.Identity.Name);
            var file = await fileService.GetFileByName(name, storageId);
            if (file != null)
            {
                string file_path = file.Path;
                string file_type = file.Type;
                string file_name = file.Name;
                return File(file_path, file_type, file_name);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> RemoveFile(string name)
        {
            string userName = User.Identity.Name;
            int storageId = await storageService.GetStorageIdByUserName(userName);
            await fileService.AddFileToTrash(storageId, name);
            var shared = await sharedService.GetByName(name, userName);
            if (shared != null) await sharedService.Delete(userName, shared.SharedLink);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> SharedFileWithLink(string name)
        {
            int storageId = await storageService.GetStorageIdByUserName(User.Identity.Name);
            var file = await fileService.GetFileByName(name, storageId);
            var getShared = await sharedService.GetByName(name, User.Identity.Name);

            if (getShared == null)
            {
                var link = Guid.NewGuid().ToString().Substring(0, 8);

                var item = new SharedFileDto()
                {
                    Name = name,
                    Type = file.Type,
                    UserName = User.Identity.Name,
                    Length = file.Length,
                    Path = file.Path,
                    SharedLink = link
                };

                await sharedService.Create(item);

                TempData["ShortLink"] = $"{name} is shared by link: " + "/sharedfile/?link=" + link;
            }
            else
            {
                TempData["ShortLink"] = $"{name} is already shared by link: " + "/sharedfile/?link=" +
                    getShared.SharedLink;
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> SearchFile(int page = 1, string _search = "")
        {
            if (string.IsNullOrEmpty(_search))
            {
                return View();
            }

            int pageSize = 20;
            IEnumerable<FileViewModel> fileCollection;
            int storageId = await storageService.GetStorageIdByUserName(User.Identity.Name);
            TempData["SearchValue"] = _search;

            fileCollection = mapper.Map<List<FileViewModel>>(
                 await fileService.SearchFiles(storageId, _search)).OrderBy(x => x.Name);

            var filesPerPages = fileCollection.Skip((page - 1) * pageSize).Take(pageSize);
            var pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = fileCollection.Count()
            };
            var resultModel = new IndexPageViewModel
            {
                PageInfo = pageInfo,
                Files = filesPerPages
            };

            return View(resultModel);
        }

        [HttpPost]
        public async Task<ActionResult> SearchFile(string search)
        {
            TempData["SearchValue"] = search;
            return await SearchFile(_search: search);
        }
    }
}