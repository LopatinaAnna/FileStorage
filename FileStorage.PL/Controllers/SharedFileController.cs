using AutoMapper;
using FileStorage.BLL.Interfaces;
using FileStorage.PL.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace FileStorage.PL.Controllers
{
    /// <summary>
    /// SharedFileController, getting shared file, access authorize and anonymous users
    /// </summary>
    [AllowAnonymous]
    public class SharedFileController : Controller
    {
        private readonly ISharedFileService sharedService;

        private readonly IMapper mapper;

        public SharedFileController(ISharedFileService _sharedService, IMapper _mapper)
        {
            sharedService = _sharedService;
            mapper = _mapper;
        }

        public async Task<ActionResult> Index(string link)
        {
            var file = await sharedService.GetSharedFile(link);
            return View(mapper.Map<SharedViewModel>(file));
        }

        [HttpPost]
        public async Task<ActionResult> GetFile(string link)
        {
            var file = await sharedService.GetSharedFile(link);
            if (file != null)
            {
                string file_path = file.Path;
                string file_type = file.Type;
                string file_name = file.Name;
                return File(file_path, file_type, file_name);
            }
            return RedirectToAction("Index");
        }
    }
}