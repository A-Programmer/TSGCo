using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.WebFrameworks.Api;
using Project.Api.ViewModels.UserViewModels;
using Project.Application.Commands.UserCommands;
using Project.Domain.Shared;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Project.Application.Queries.UserQueries;
using System.Security.Claims;
using Project.Domain.Shared.Utilities;
using Project.Application.Queries.RoleQueries;
using Project.Application.Commands.RoleCommands;
using Project.Domain.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Api.Controllers
{
    [ApiVersion("1")]
    public class accountController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly PublicSettings _settings;

        public accountController(IMediator mediator,
            IOptionsSnapshot<PublicSettings> optionsSnapshot)
        {
            _settings = optionsSnapshot.Value;
            _mediator = mediator;
        }

        [HttpPost(Routes.Account.Login)]
        public async Task<ActionResult> Login([FromForm]login_vm model)
        {
            //Check UserName and Password Existance
            var loginCommand = new ValidateUserCommand(model.username, model.password);
            var loginResult = await _mediator.Send(loginCommand);

            if (loginResult == null)
                return CustomError(Status.BadRequest, "نام کاربری یا کلمه عبور صحیح نمی باشد");

            var updateSecurityStampCommand = new UpdateSecurityStampCommand(loginResult.Id);
            var updateSecurityStampResult = await _mediator.Send(updateSecurityStampCommand);

            if (!updateSecurityStampResult)
                return CustomError(Status.ServerError, "خطای سرور رخ داده است.");

            var generateTokenCommand = new GenerateAccessTokenCommand(model.username, _settings.JwtOptions);
            var accessTokenResult = await _mediator.Send(generateTokenCommand);

            //GenerateToken
            var accessTokenVm = new access_token_vm(accessTokenResult.Access_Token, accessTokenResult.Refresh_Token, accessTokenResult.Token_Type, accessTokenResult.Expires_In);

            return new JsonResult(accessTokenVm);
        }



        [HttpPost(Routes.Account.Register)]
        public async Task<ActionResult> Register(register_vm model)
        {
            var existingByEmailQuery = new GetUserByEmailQuery(model.email);
            var existingByEmailResult = await _mediator.Send(existingByEmailQuery);
            if (existingByEmailResult != null)
                return CustomError(Status.DuplicateRecord, "ایمیل تکراری است.");


            var existingByUserNameQuery = new GetUserByUserNameQuery(model.user_name);
            var existingByUserNameResult = await _mediator.Send(existingByUserNameQuery);
            if (existingByUserNameResult != null)
                return CustomError(Status.DuplicateRecord, "نام کاربری تکراری است.");


            var existingByPhoneQuery = new GetUserByPhoneQuery(model.phone_number);
            var existingByPhoneResult = await _mediator.Send(existingByPhoneQuery);
            if (existingByPhoneResult != null)
                return CustomError(Status.DuplicateRecord, "شماره تماس تکراری است.");

            var registerCommand = new RegisterCommand(model.user_name, model.password, model.email, model.phone_number, true);
            var registerResult = await _mediator.Send(registerCommand);

            if (registerResult == null)
                return CustomError(Status.ServerError, "خطای سرور رخ داده است");

            var roleQuery = new GetRoleByNameQuery("User");
            var roleResult = await _mediator.Send(roleQuery);

            if (roleResult == null)
            {
                var addRoleCommand = new AddRoleCommand("User", "Users");
                var addRoleResult = await _mediator.Send(addRoleCommand);

                var addToRoleCommand = new AddUserToRoleCommand(registerResult.Id, new Guid[] { addRoleResult.Id } );
                var addToRoleResult = await _mediator.Send(addToRoleCommand);
            }
            else
            {
                var addToRoleCommand = new AddUserToRoleCommand(registerResult.Id, new Guid[] { roleResult.Id });
                var addToRoleResult = await _mediator.Send(addToRoleCommand);
            }

            //Insert admin role if does not exist
            var adminRoleQuery = new GetRoleByNameQuery("Admin");
            var adminRoleResult = await _mediator.Send(adminRoleQuery);

            if (adminRoleResult == null)
            {
                var addRoleCommand = new AddRoleCommand("Admin", "Admins");
                var addRoleResult = await _mediator.Send(addRoleCommand);
                if(model.email.ToLower() == "mrsadin@gmail.com")
                {
                    var addToRoleCommand = new AddUserToRoleCommand(registerResult.Id, new Guid[] { addRoleResult.Id });
                    var addToRoleResult = await _mediator.Send(addToRoleCommand);
                }
            }
            else
            {
                if (model.email.ToLower() == "mrsadin@gmail.com")
                {
                    var addToRoleCommand = new AddUserToRoleCommand(registerResult.Id, new Guid[] { adminRoleResult.Id });
                    var addToRoleResult = await _mediator.Send(addToRoleCommand);
                }
            }

            return CustomOk(registerResult);
        }

        [Authorize]
        [HttpPost(Routes.Account.IsValidUser)]
        public async Task<IActionResult> IsValidUser()
        {

            var currentUser = HttpContext.User;
            if (currentUser == null)
                return Unauthorized();

            if (currentUser.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
            {
                var stringId = currentUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

                var id = Guid.Parse(stringId);
                var query = new GetUserQuery(id);

                var result = await _mediator.Send(query);

                if (result == null)
                    return CustomError(Status.NotFound, "کاربر مورد نظر یافت نشد");

                return CustomOk(result, "ok");
            }
            else
            {
                return CustomOk(null, "OK");
            }
            
        }

        [Authorize]
        [HttpPost(Routes.Account.Logout)]
        public async Task<IActionResult> Logout()
        {

            var currentUser = HttpContext.User;
            if (currentUser == null)
                return Unauthorized();

            var stringUserId = currentUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            if (stringUserId == null)
                return CustomError(Status.BadRequest, "اطلاعات کاربر یافت نشد");

            var userId = new Guid();
            var guidConvertResult = Guid.TryParse(stringUserId, out userId);

            if (!guidConvertResult)
                return CustomError(Status.BadRequest, "کاربر مورد نظر یافت نشد.");

            var command = new UpdateSecurityStampCommand(userId);

            var result = await _mediator.Send(command);

            return CustomOk(result);
        }



        [HttpPost(Routes.Account.RefreshToken)]
        public async Task<IActionResult> RefreshToken(refresh_token_vm refreshTokenVm)
        {
            var query = new GetUserByRefreshTokenQuery(refreshTokenVm.refresh_token);
            var result = await _mediator.Send(query);

            if (result == null)
                return CustomError(Status.NotFound, "کد بازیابی صحیح نمی باشد");

            var getAccessTokenCommand = new GenerateAccessTokenCommand(result.UserName, _settings.JwtOptions);
            var generateAccessTokenResult = await _mediator.Send(getAccessTokenCommand);

            var accessTokenVm = new access_token_vm(generateAccessTokenResult.Access_Token, generateAccessTokenResult.Refresh_Token, generateAccessTokenResult.Token_Type, generateAccessTokenResult.Expires_In);

            return new JsonResult(accessTokenVm);
        }

        [Authorize]
        [HttpGet(Routes.Account.Profile)]
        [ProducesResponseType(typeof(user_profile_vm), 200)]
        public async Task<IActionResult> Profile()
        {
            var isLoggedIn = HttpContext.User.Identity.IsAuthenticated;

            if (!isLoggedIn)
                return CustomError(Status.NotAuthenticated, "احازه دسترسی ندارید");

            var userId = HttpContext.User.Identity.GetUserId();

            if(string.IsNullOrEmpty(userId))
                return CustomError(Status.NotAuthenticated, "احازه دسترسی ندارید");


            var convertResult = Guid.TryParse(userId, out Guid id);

            if (!convertResult)
                return CustomError(Status.NotAuthenticated, "اجازه دسترسی ندارید");

            var query = new GetProfileQuery(id);

            var result = await _mediator.Send(query);

            if(result == null)
                return CustomError(Status.NotFound, "کاربر مورد نظر یافت نشد");

            var profile = new user_profile_vm(result.Id, result.UserName, result.Email, result.PhoneNumber, result.FirstName, result.LastName, result.FullName, result.AboutMe, result.BirthDate, result.Roles, result.ProfileImageUrl);

            return CustomOk(profile);
        }

        [Authorize]
        [HttpPost(Routes.Account.UpdateProfile)]
        public async Task<IActionResult> UpdateProfile(update_profile_vm profile)
        {
            var isLoggedIn = HttpContext.User.Identity.IsAuthenticated;

            if (!isLoggedIn)
                return CustomError(Status.NotAuthenticated, "اجازه دسترسی ندارید");

            var userId = HttpContext.User.Identity.GetUserId();

            if (string.IsNullOrEmpty(userId))
                return CustomError(Status.NotAuthenticated, "اجازه دسترسی ندارید");


            var convertResult = Guid.TryParse(userId, out Guid id);

            if (!convertResult)
                return CustomError(Status.NotAuthenticated, "اجازه دسترسی ندارید");

            if (id != profile.user_id)
                return CustomError(Status.BadRequest, "شناسه کاربر با شناسه کاربر احراز هویت شده متفاوت است");

            var command = new UpdateProfileCommand(profile.user_id, profile.user_name, profile.email, profile.phone_number,profile.first_name, profile.last_name, profile.profile_image_url, profile.about_me, profile.birth_date);

            var result = await _mediator.Send(command);

            return CustomOk(result);
        }

        [Authorize]
        [HttpPost(Routes.Account.ChangePassword)]
        public async Task<IActionResult> ChangePassword(change_password_vm model)
        {
            var isLoggedIn = HttpContext.User.Identity.IsAuthenticated;

            if (!isLoggedIn)
                return CustomError(Status.NotAuthenticated, "اجازه دسترسی ندارید");

            var isAdmin = HttpContext.User.IsInRole("admin");

            if(!isAdmin)
            {

                var userId = HttpContext.User.Identity.GetUserId();

                if (string.IsNullOrEmpty(userId))
                    return CustomError(Status.NotAuthenticated, "اجازه دسترسی ندارید");


                var convertResult = Guid.TryParse(userId, out Guid id);

                if (!convertResult)
                    return CustomError(Status.NotAuthenticated, "اجازه دسترسی ندارید");

                if (id != model.id)
                    return CustomError(Status.BadRequest, "شناسه کاربر با شناسه کاربر احراز هویت شده متفاوت است");
            }


            var command = new ChangePasswordCommand(model.id, model.user_name, model.current_password, model.new_password);

            var result = await _mediator.Send(command);

            return CustomOk(result);
        }

    }
}
