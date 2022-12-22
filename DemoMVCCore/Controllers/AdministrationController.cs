using DemoMVCCore.Model;
using DemoMVCCore.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoMVCCore.Controllers
{
    //[Authorize(Roles = "admin,User")] // either
    [Authorize(Roles = "admin")] // both
    [Authorize(Roles = "User")] // both
    [Route("[controller]")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AdministrationController> _logger;

        public AdministrationController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            ILogger<AdministrationController> logger)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._logger = logger;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await this._userManager.FindByIdAsync(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id ={user.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                var result = await this._userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListUsers");
            }
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string id)
        {
            //throw new Exception("Test Exception");
            var role = await this._roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id ={role.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                try
                {
                    var result = await this._roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListRoles");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View("ListRoles");
                }
                catch (DbUpdateException ex)
                {
                    // logging
                    this._logger.LogError($"Error deleting role:{ex}");
                    ViewBag.ErrorTitle = $"{role.Name} role is in use";
                    ViewBag.ErrorMessage = $"{role.Name} role cannot be deleted as there are users in the role. If you want to delete this role, " +
                        $"please remove the users from the role and then try again";

                    return View("Error");
                }
                catch (Exception ex)
                {
                    return View("Error");
                }
            }
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                IdentityRole idRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                IdentityResult ret = await _roleManager.CreateAsync(idRole);

                if (ret.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }
                foreach(IdentityError err in ret.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(model);
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = this._roleManager.Roles;
            return View(roles);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var oRet = await this._roleManager.FindByIdAsync(id);
            if(oRet == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                Id = oRet.Id,
                RoleName = oRet.Name
            };

            foreach(var user in _userManager.Users)
            {
                if(await _userManager.IsInRoleAsync(user, oRet.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {

            var oRole = await this._roleManager.FindByIdAsync(model.Id);
            if (oRole == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                oRole.Name = model.RoleName;
                var ret = await this._roleManager.UpdateAsync(oRole);

                if (ret.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach(var err in ret.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }

                return View(model);
            }
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await this._roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();
            foreach(var user in this._userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                userRoleViewModel.IsSelected = false;
                if (await this._userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }

                model.Add(userRoleViewModel);
            }
            return View(model);
            
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {

            var role = await this._roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            foreach(var user in model)
            {
                if (user.UserId == null) continue;
                var oUser = await _userManager.FindByIdAsync(user.UserId);
                IdentityResult result = null;
                if (user.IsSelected && !(await this._userManager.IsInRoleAsync(oUser, role.Name)))
                {
                    result = await this._userManager.AddToRoleAsync(oUser, role.Name);
                }
                else if (!user.IsSelected && (await this._userManager.IsInRoleAsync(oUser, role.Name)))
                {
                    result = await this._userManager.RemoveFromRoleAsync(oUser, role.Name);
                }
                else continue;

                if (!result.Succeeded)
                {
                    continue;
                }
            }
            return RedirectToAction("EditRole", new { id = roleId });

        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult ListUsers()
        {
            var users = this._userManager.Users;
            return View(users);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> EditUser(string id)
        {
            var user = await this._userManager.FindByIdAsync(id);

            if (user==null)
            {
                ViewBag.ErrorMessage = $"User with Id ={id} cannot be found";
                return View("NotFound");
            }

            var userClaims = await this._userManager.GetClaimsAsync(user);
            var userRoles = await this._userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                City = user.City,
                Claims = userClaims.Select(c => c.Value).ToList(),
                Roles = userRoles
            };

            return View(model);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {

            if (model == null)
            {
                ViewBag.ErrorMessage = $"User cannot be found";
                return View("NotFound");
            }

            var user = await this._userManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.City = model.City;

                var result = await this._userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> ManageUserRoles(string id)
        {
            ViewBag.userId = id;
            var model = new List<UserRolesViewModel>();

            var user = await this._userManager.FindByIdAsync(id);
            if(user == null)
            {
                ViewBag.ErrorMessage = $"User cannot be found";
                return View("NotFound");
            }

            foreach (var role in _roleManager.Roles)
            {
                var oRole = new UserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    IsSelected = false

                };

                if(await this._userManager.IsInRoleAsync(user, role.Name))
                {
                    oRole.IsSelected = true;
                }
                model.Add(oRole);
            }
            return View(model);

        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> ManageUserRoles(List<UserRolesViewModel >model, string userId)
        {
            if (model == null)
            {
                ViewBag.ErrorMessage = $"User cannot be found";
                return View("NotFound");
            }

            var user = await this._userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            else
            {

                var roles = await this._userManager.GetRolesAsync(user);
                var result = await this._userManager.RemoveFromRolesAsync(user, roles);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Cannot remove user existing roles");
                    return View(model);
                }

                result = await this._userManager.AddToRolesAsync(user, model.Where(x => x.IsSelected).Select(y => y.RoleName));
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Cannot add selected roles to user");
                    return View(model);
                }
            }

            return RedirectToAction("EditUser", new { Id = userId });

        }

    }
}
