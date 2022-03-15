

using Microsoft.AspNetCore.Mvc;

namespace Project.Auth.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }


        public async Task<IActionResult> Samples()
        {
            return View();
        }
    }
}