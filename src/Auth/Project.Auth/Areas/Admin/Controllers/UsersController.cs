

using System.ComponentModel;
using KSFramework.Pagination;
using Microsoft.AspNetCore.Mvc;
using Project.Auth.Domain;
using Project.Auth.Services;

namespace Project.Auth.Areas.Admin.Controllers
{
    [Area("Admin")]
    [DisplayName("Manage Users")]
    public class UsersController : Controller
    {
        private readonly IUserServices _userServices;
        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        [DisplayName("Users List")]
        public async Task<IActionResult> Index(int? id, string currentFilter, string searchString)
        {
            var page = id ?? 1;
            var pageSize = 1;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var items = _userServices.GetUsers();

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
    }
}