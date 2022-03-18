

using System.ComponentModel;
using IdentityServer4.Models;
using KSFramework.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Auth.Data;
using Project.Auth.Domain;
using Project.Auth.Extensions;
// using Project.Auth.Services;

namespace Project.Auth.Areas.Admin.Controllers
{
    [Area("Admin")]
    [DisplayName("Manage Users")]
    public class UsersController : Controller
    {
        // private readonly IUserServices _userServices;
        private readonly ApplicationDbContext _db;
        public UsersController(ApplicationDbContext db)//IUserServices userServices)
        {
            // _userServices = userServices;
            _db = db;
        }

        [HttpGet, DisplayName("Users List"),Authorize(Roles = "admin")]
        public async Task<IActionResult> Index(int? id, string currentFilter, string searchString)
        {
            Console.WriteLine($"\n\n\n\n\n{"admin".Sha256()}\n\n\n\n\n{"user".Sha256()}\n\n\n\n\n\n\n");
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

            var items = _db.Users.AsQueryable();

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

            return PartialView("_ListPartialView", pagedItems);
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
    }
}