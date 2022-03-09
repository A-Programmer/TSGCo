using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using Project.Auth.Domain;
using Project.Auth.Services;
using Project.Auth.Utilities;
using Project.Auth.ViewModels.UserRegistration;


namespace Project.Auth.Controllers
{
    public class UserRegistrationController : Controller
    {
        private readonly IUsersService _usersService;

        public UserRegistrationController(IUsersService usersService)
        {
            _usersService = usersService;
        }


        [HttpGet]
        public IActionResult RegisterUser()
        {
            var userToRegister = new RegisterUserViewModel();
            return View(userToRegister);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var subjectId = Guid.NewGuid();

            var userToCreate = new User
            {
                SubjectId = subjectId.ToString(),
                Password = model.Password.GetSha256Hash(),
                Username = model.Username,
                IsActive = true
            };

            userToCreate.UserClaims.Add(new UserClaim(userToCreate.SubjectId ,"address", model.Address));
            userToCreate.UserClaims.Add(new UserClaim(userToCreate.SubjectId, "given_name", model.Firstname));
            userToCreate.UserClaims.Add(new UserClaim(userToCreate.SubjectId, "family_name", model.Lastname));
            userToCreate.UserClaims.Add(new UserClaim(userToCreate.SubjectId, "subscription", "user"));
            userToCreate.UserClaims.Add(new UserClaim(userToCreate.SubjectId, "country", model.Country));

            await _usersService.AddUserAsync(userToCreate);

            return Redirect("~/");
        }
    }
}
