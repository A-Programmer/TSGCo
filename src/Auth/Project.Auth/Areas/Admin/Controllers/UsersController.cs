

using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace Project.Auth.Areas.Admin.Controllers
{
    [Area("Admin")]
    [DisplayName("Manage Users")]
    public class UsersController : Controller
    {
        public UsersController()
        {
            
        }

        [HttpGet]
        [DisplayName("Users List")]
        public async Task<IActionResult> Index(int? id, string currentFilter, string searchString,
        int? filterType,
        int? statusType)
        {
            var page = id ?? 1;
            var pageSize = 20;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;
            ViewData["filterType"] = filterType;
            ViewData["statusType"] = statusType;

            var isAjax = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
            if(isAjax)
                return PartialView("_ListPartialView");
            return View();
        }
    }
}