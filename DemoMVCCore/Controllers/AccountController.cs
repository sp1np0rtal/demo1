using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoMVCCore.Model;
using DemoMVCCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DemoMVCCore.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        // GET: /<controller>/
        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [AllowAnonymous]
        [Route("[action]")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("[action]")]
        //[HttpPost]
        //[HttpGet]
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var ret = await _userManager.FindByEmailAsync(email);
            if (ret == null)
            {
                return Json(true);
            }
            return (Json($"Email {ret} is already in use"));
        }

        [AllowAnonymous]
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    City = model.City
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (this._signInManager.IsSignedIn(User) && User.IsInRole("admin"))
                    {
                        return RedirectToAction("ListUsers", "Administration");
                    }
                        await this._signInManager.SignInAsync(user, isPersistent: false); // session cookie
                        return RedirectToAction("List", "Home", new HomeListViewModel());
                    
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Logout(RegisterViewModel model)
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("List", "Home", new HomeListViewModel());
        }

        [AllowAnonymous]
        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("List", "Home", new HomeListViewModel());
        }

        [AllowAnonymous]
        [Route("[action]")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                        return LocalRedirect(ReturnUrl); // security hole "Open Redirect Attack" on querystring; not checking if local
                    return RedirectToAction("List", "Home", new HomeListViewModel());
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(model);
        }

        [AllowAnonymous]
        [Route("[action]")]
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
