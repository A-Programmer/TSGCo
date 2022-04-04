using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.CMS.Services;

namespace Project.CMS.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenServices _tokenServices;
        public AccountsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LogOut()
        {
            return SignOut("Cookies", "oidc");
        }

    }
}
