

using System.ComponentModel;
using IdentityServer4.Models;
using KSFramework.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Auth.Areas.Admin.ViewModels;
using Project.Auth.Areas.Admin.ViewModels.Users;
using Project.Auth.Data;
using Project.Auth.Domain;
using Project.Auth.Extensions;
using Project.Auth.Models;
using Project.Auth.Services;
using static IdentityServer4.IdentityServerConstants;
// using Project.Auth.Services;

namespace Project.Auth.Areas.Admin.Controllers
{
    [Area("Admin")]
    [DisplayName("Manage Users")]
    public class UsersController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _db;
        public UsersController(IUserServices userServices, UserManager<User> userManager)
        {
            _userServices = userServices;
            _userManager = userManager;
        }

        [HttpGet, DisplayName("Users List")]
        public async Task<IActionResult> Index(int? id, string currentFilter, string searchString)
        {
            var page = id ?? 1;
            var pageSize = 50;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var items = _userServices.GetQueryable().Include(x => x.Profile).AsQueryable();

            if(!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(x =>
                    x.UserName.ToLower().Contains(searchString) ||
                    x.Profile.FirstName.ToLower().Contains(searchString) ||
                    x.Profile.LastName.ToLower().Contains(searchString)
                );
            }

            var pagedItems = await PaginatedList<User>.CreateAsync(items, page, pageSize);

            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if(isAjax)
                return PartialView("_ListPartialView", pagedItems);
            return View(pagedItems);
        }


        #region Add User
        [HttpGet, DisplayName("Add User")]
        public IActionResult Add()
        {
            return PartialView("_AddPartialView");
        }

        [HttpPost, ValidateAntiForgeryToken, DisplayName("Adding User"), ActionName("Add")]
        public async Task<IActionResult> AddUser(AddUserViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return PartialView("_AddPartialView", model);
            }
            var user = new User()
            {
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                IsActive = model.IsActive
            };
            if(!string.IsNullOrEmpty(model.FirstName) || !string.IsNullOrEmpty(model.LastName))
            {
                var profile = new UserProfile(model.FirstName, model.LastName);
                user.SetProfile(profile);
            }
            var registerationResult = await _userManager.CreateAsync(user, model.Password);
            if(!registerationResult.Succeeded)
            {
                foreach(var err in registerationResult.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return PartialView("_AddPartialView", model);
            }
            var result = new ResultMessage(false, "warning", "");
            TempData.Put("Message", result);
            return PartialView("_AddPartialView", model);
        }
        #endregion

        #region Update User
        [HttpGet, DisplayName("Update User")]
        public async Task<IActionResult> Update(Guid id)
        {
            var user = await _userServices.GetById(id);

            var userVm = new UpdateUserViewModel
            {
                Id = user.Id,
                Email = user.UserName,
                PhoneNumber = user.PhoneNumber,
                IsActive = user.IsActive
            };

            if(user.Profile != null)
            {
                userVm.FirstName = user.Profile.FirstName;
                userVm.LastName = user.Profile.LastName;
            }

            return PartialView("_UpdatePartialView", userVm);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("Update"), DisplayName("Updating User")]
        public async Task<IActionResult> UpdatePost(UpdateUserViewModel userVm)
        {
            var result = new ResultMessage(false, "warning", "");

            var user = await _userManager.FindByIdAsync(userVm.Id.ToString());

            user.Id = userVm.Id;
            user.UserName = userVm.Email;
            user.Email = userVm.Email;
            user.PhoneNumber = userVm.PhoneNumber;
            user.IsActive = userVm.IsActive;
            
            if(ModelState.IsValid)
            {
                var updateResult = await _userManager.UpdateAsync(user);
                if(!updateResult.Succeeded)
                {
                    result.CssClass = "danger";
                    result.Status = false;
                    result.Message = "خطا در ویرایش کاربر.";
                    foreach(var err in updateResult.Errors)
                    {
                        ModelState.AddModelError("", err.Description);
                    }
                }
                else
                {
                    var userProfile = new UpdateUserProfileViewModel
                    {
                        UserId = userVm.Id,
                        FirstName = userVm.FirstName,
                        LastName = userVm.LastName
                    };
                    await _userServices.UpdateUserProfile(userProfile);
                    result.CssClass = "success";
                    result.Status = true;
                    result.Message = "کاربر مورد نظر با موفقیت ویرایش شد.";
                }
            }
            else
            {
                result.CssClass = "warning";
                result.Message = "لطفا فرم مورد نظر را با دقت پر کنید.";
                result.Status = false;
            }

            TempData.Put("Message", result);
            
            return PartialView("_UpdatePartialView", userVm);
        }
        #endregion

        #region Change User Status
        [HttpGet]
        public IActionResult ChangeStatus(Guid id)
        {
            return PartialView("_ChangeStatusPartialView", id);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("ChangeStatus")]
        public async Task<IActionResult> ChangeStatusPost(Guid id)
        {
            var ressult = await _userServices.ChangeUserStatus(id);
            TempData.Put("Message", ressult);
            return PartialView("_ChangeStatusPartialView", id);
        }
        #endregion

        #region Delete User
        [HttpGet, DisplayName("Delete User")]
        public IActionResult Delete(Guid id)
        {
            return PartialView("_DeletePartialView", id);
        }

        [HttpPost, ValidateAntiForgeryToken, DisplayName("Deleting User"), ActionName("Delete")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var result = new ResultMessage(false, "warning", "");

            var user = await _userManager.FindByIdAsync(id.ToString());
            if(user != null)
            {
                var deleteResult = await _userManager.DeleteAsync(user);
                result.Update(true, "success", "کاربر مورد نظر با موفقیت حذف شد.");
            }
            else
            {
                result.Update(false, "danger", "کاربر مورد نظر یافت نشد.");
            }
            TempData.Put("Message", result);
            return PartialView("_DeletePartialView", id);
        }
        #endregion

        #region RemoveProfile
        [HttpGet]
        public IActionResult RemoveProfile(Guid id)
        {
            return PartialView("_RemoveProfilePartialView", id);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("RemoveProfile")]
        public async Task<IActionResult> RemoveProfilePost(Guid id)
        {
            var result = await _userServices.RemoveUserProfile(id);
            TempData.Put("Message", result);

            return PartialView("_RemoveProfilePartialView", id);
        }
        #endregion


        #region Add Profile
        [HttpGet]
        public IActionResult AddProfile(Guid id)
        {
            var profile = new AddUserProfileViewModel() { UserId = id };
            return PartialView("_AddUserProfilePartialView", profile);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("AddProfile")]
        public async Task<IActionResult> AddProfilePost(AddUserProfileViewModel userProfileViewModel)
        {
            var result = new ResultMessage(false, "warning", "");
            
            if(ModelState.IsValid)
            {
                result = await _userServices.AddUserProfile(userProfileViewModel);
            }
            else
            {
                result.CssClass = "warning";
                result.Message = "لطفا فرم مورد نظر را با دقت پر کنید.";
                result.Status = false;
            }

            TempData.Put("Message", result);
            
            return PartialView("_AddUserProfilePartialView", userProfileViewModel);
        }
        #endregion
        
        #region Update Profile
        [HttpGet]
        public async Task<IActionResult> UpdateProfile(Guid id)
        {
            var profile = new UpdateUserProfileViewModel() { UserId = id };
            var user = await _userServices.GetById(id);
            if(user == null)
            {
                ModelState.AddModelError("", "کاربر مورد نظر یافت نشد.");
                return PartialView("_UpdateUserProfilePartialView");
            }
            if(user.Profile != null)
            {
                profile.FirstName = user.Profile.FirstName;
                profile.LastName = user.Profile.LastName;
            }
            return PartialView("_UpdateUserProfilePartialView", profile);
        }

        [HttpPost, ValidateAntiForgeryToken, ActionName("UpdateProfile")]
        public async Task<IActionResult> UpdateProfilePost(UpdateUserProfileViewModel userProfileViewModel)
        {
            var result = new ResultMessage(false, "warning", "");
            
            if(ModelState.IsValid)
            {
                result = await _userServices.UpdateUserProfile(userProfileViewModel);
            }
            else
            {
                result.CssClass = "warning";
                result.Message = "لطفا فرم مورد نظر را با دقت پر کنید.";
                result.Status = false;
            }

            TempData.Put("Message", result);
            
            return PartialView("_UpdateUserProfilePartialView", userProfileViewModel);
        }
        #endregion
    
    
    }
}