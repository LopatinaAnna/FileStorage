using FileStorage.BLL.DTO;
using FileStorage.BLL.Infrastructure;
using FileStorage.BLL.Interfaces;
using FileStorage.PL.Models;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FileStorage.PL.Controllers
{
    /// <summary>
    /// AccountController, controll login and register
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService _userService)
        {
            userService = _userService;
        }

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDto userDto = new UserDto { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await userService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Wrong login or password.");
                }
                else
                {
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDto userDto = new UserDto
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Password = model.Password,
                    Role = "user"
                };
                OperationDetails operationDetails = await userService.Create(userDto);
                ClaimsIdentity claim = await userService.Authenticate(userDto);
                AuthenticationManager.SignIn(new AuthenticationProperties
                {
                    IsPersistent = true
                }, claim);
                if (operationDetails.Succedeed)
                    return RedirectToAction("Index", "Home");
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        private async Task SetInitialDataAsync()
        {
            await userService.SetInitialData(new UserDto
            {
                Email = "admin@gmail.com",
                UserName = "admin@gmail.com",
                Password = "qwerty",
                Role = "admin",
            }, new List<string> { "user", "admin" });
        }
    }
}