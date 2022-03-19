

using System.ComponentModel;
using IdentityServer4.Models;
using KSFramework.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Auth.Areas.Admin.ViewModels.Users;
using Project.Auth.Data;
using Project.Auth.Domain;
using Project.Auth.Extensions;
using Project.Auth.Models;
using static IdentityServer4.IdentityServerConstants;
// using Project.Auth.Services;

namespace Project.Auth.Areas.Admin.Controllers
{
    [Area("Admin")]
    [DisplayName("Manage Users")]
    public class UsersController : Controller
    {
        // private readonly IUserServices _userServices;
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _db;
        public UsersController(ApplicationDbContext db)//IUserServices userServices)
        {
            // _userServices = userServices;
            _db = db;
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

            var items = _db.Users.Include(x => x.Profile).AsQueryable();

            if(!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(x =>
                    x.UserName.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            var pagedItems = await PaginatedList<User>.CreateAsync(items, page, pageSize);

            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if(isAjax)
                return PartialView("_ListPartialView", pagedItems);
            return View(pagedItems);
        }


        #region Add User
        [HttpGet, DisplayName("Add User")]
        public async Task<IActionResult> Add()
        {
            // var roles = await _roleManager.Roles
            //     .Where(x => x.Name.ToLower() != "admin")
            //     .OrderBy(r => r.Title).ToListAsync();
            // ViewBag.Roles = new SelectList(roles, "Name", "Title");
            return PartialView("_AddPartialView");
        }

        [HttpPost, ValidateAntiForgeryToken, DisplayName("Adding User"), ActionName("Add")]
        public async Task<IActionResult> AddUser()//AddUserViewModel model)
        {
            // var roles = await _roleManager.Roles
            //     .Where(x => x.Name.ToLower() != "admin")
            //     .OrderBy(r => r.Title).ToListAsync();
            // ViewBag.Roles = new SelectList(roles, "Name", "Title");

            // if (ModelState.IsValid)
            // {
            //     var result = await _userServic.CreateUser(model);
            //     TempData.Put("Message", result);
            // }
            return PartialView("_AddPartialView");//model);
        }
        #endregion

        #region Change User Status
        [HttpGet]
        public IActionResult ChangeStatus(Guid id)
        {
            return PartialView("_ChangeStatusPartialView", id);
        }

        [HttpPost, ActionName("ChangeStatus")]
        public async Task<IActionResult> ChangeStatusPost(Guid id)
        {
            // var ressult = await _userServices.ChangeStatus(id);
            // TempData.Put("Message", ressult);
            return PartialView("_ChangeStatusPartialView", id);
        }
        #endregion


        #region RemoveProfile
        [HttpGet]
        public IActionResult RemoveProfile(Guid id)
        {
            return PartialView("_RemoveProfilePartialView", id);
        }

        [HttpPost, ActionName("RemoveProfile")]
        public async Task<IActionResult> RemoveProfilePost(Guid id)
        {
            var rm = new ResultMessage()
            {
                Status = false,
                Message = "",
                CssClass = "warning"
            };

            var user = await _db.Users.Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == id);
            if(user != null)
            {
                user.RemoveProfile();
                rm.CssClass = "success";
                rm.Message = "عملیات با موفقیت انجام شد";
                rm.Status = true;
            }
            else
            {

                rm.CssClass = "warning";
                rm.Message = "کاربر مورد نظر یافت نشد.";
                rm.Status = true;
            }
            await _db.SaveChangesAsync();
            TempData.Put("Message", rm);

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

        [HttpPost, ActionName("AddProfile")]
        public async Task<IActionResult> AddProfilePost(AddUserProfileViewModel userProfileViewModel)
        {
            var rm = new ResultMessage()
            {
                Status = false,
                Message = "",
                CssClass = "warning"
            };
            
            if(ModelState.IsValid)
            {
                var user = await _db.Users.Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == userProfileViewModel.UserId);
                if(user != null)
                {
                    user.RemoveProfile();
                    var userProfile = new UserProfile(userProfileViewModel.FirstName, userProfileViewModel.LastName);
                    // user.SetProfileId(userProfile.Id);
                    user.SetProfile(userProfile);

                    await _db.SaveChangesAsync();

                    rm.CssClass = "success";
                    rm.Message = "پروفایل کاربر با موفقیت صبت شد.";
                    rm.Status = true;
                }
                else
                {
                    rm.CssClass = "warning";
                    rm.Message = "کاربر مورد نظر یافت نشد.";
                    rm.Status = false;
                }
            }
            else
            {
                rm.CssClass = "warning";
                rm.Message = "لطفا فرم مورد نظر را با دقت پر کنید.";
                rm.Status = false;
            }

            TempData.Put("Message", rm);
            
            return PartialView("_AddUserProfilePartialView", userProfileViewModel);
        }
        #endregion
    }
}