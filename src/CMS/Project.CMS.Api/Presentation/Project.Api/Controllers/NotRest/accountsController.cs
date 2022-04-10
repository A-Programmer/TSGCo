using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.WebFrameworks.Api;
using Project.Api.ViewModels.UserViewModels;
using Project.Application.Commands.UserCommands;
using Microsoft.Extensions.Configuration;
using Project.Domain.Common;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using Project.Application.Queries.UserQueries;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Project.Common.Utilities;
using Project.Application.Queries.RoleQueries;
using Project.Application.Commands.RoleCommands;
using Project.Api.ViewModels;
using Project.Domain.Enums;
using Project.Common;
using Project.Common.Exceptions;
using System.Net;


namespace Project.Api.Controllers
{
    [ApiVersion("1")]
    public class accountsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly PublicSettings _settings;

        public accountsController(IMediator mediator,
            IOptionsSnapshot<PublicSettings> optionsSnapshot)
        {
            _settings = optionsSnapshot.Value;
            _mediator = mediator;
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
                    return CustomError(Common.Status.NotFound, "کاربر مورد نظر یافت نشد");

                return CustomOk(result, "ok");
            }
            else
            {
                return CustomOk(null, "OK");
            }

        }



        [HttpPost(Routes.Account.RefreshToken)]
        public async Task<IActionResult> RefreshToken(refresh_token_vm refreshTokenVm)
        {
            var query = new GetUserByRefreshTokenQuery(refreshTokenVm.refresh_token);
            var result = await _mediator.Send(query);

            if (result == null)
                return CustomError(Common.Status.NotFound, "کد بازیابی صحیح نمی باشد");

            var getAccessTokenCommand = new GenerateAccessTokenCommand(result.UserName, _settings.JwtOptions);
            var generateAccessTokenResult = await _mediator.Send(getAccessTokenCommand);

            var accessTokenVm = new access_token_vm(generateAccessTokenResult.Access_Token, generateAccessTokenResult.Refresh_Token, generateAccessTokenResult.Token_Type, generateAccessTokenResult.Expires_In);

            return new JsonResult(accessTokenVm);
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
                return CustomError(Common.Status.BadRequest, "اطلاعات کاربر یافت نشد");

            var userId = new Guid();
            var guidConvertResult = Guid.TryParse(stringUserId, out userId);

            if (!guidConvertResult)
                return CustomError(Common.Status.BadRequest, "کاربر مورد نظر یافت نشد.");

            var command = new UpdateSecurityStampCommand(userId);

            var result = await _mediator.Send(command);

            return CustomOk(result);
        }

        [Authorize]
        [HttpGet(Routes.Account.Profile)]
        [ProducesResponseType(typeof(user_profile_vm), 200)]
        public async Task<IActionResult> Profile()
        {
            var isLoggedIn = HttpContext.User.Identity.IsAuthenticated;

            if (!isLoggedIn)
                return CustomError(Common.Status.NotAuthenticated, "احازه دسترسی ندارید");

            var userId = HttpContext.User.Identity.GetUserId();

            if(string.IsNullOrEmpty(userId))
                return CustomError(Common.Status.NotAuthenticated, "احازه دسترسی ندارید");


            var convertResult = Guid.TryParse(userId, out Guid id);

            if (!convertResult)
                return CustomError(Common.Status.NotAuthenticated, "اجازه دسترسی ندارید");

            var query = new GetProfileQuery(id);

            var result = await _mediator.Send(query);

            if(result == null)
                return CustomError(Common.Status.NotFound, "کاربر مورد نظر یافت نشد");

            var profile = new user_profile_vm(result.Id, result.UserName, result.Email, result.PhoneNumber, result.FirstName, result.LastName, result.FullName, result.AboutMe, result.BirthDate, result.Roles, result.ProfileImageUrl);

            return CustomOk(profile);
        }

        [Authorize]
        [HttpPost(Routes.Account.UpdateProfile)]
        public async Task<IActionResult> UpdateProfile(update_profile_vm profile)
        {
            var isLoggedIn = HttpContext.User.Identity.IsAuthenticated;

            if (!isLoggedIn)
                return CustomError(Common.Status.NotAuthenticated, "اجازه دسترسی ندارید");

            var userId = HttpContext.User.Identity.GetUserId();

            if (string.IsNullOrEmpty(userId))
                return CustomError(Common.Status.NotAuthenticated, "اجازه دسترسی ندارید");


            var convertResult = Guid.TryParse(userId, out Guid id);

            if (!convertResult)
                return CustomError(Common.Status.NotAuthenticated, "اجازه دسترسی ندارید");

            if (id != profile.user_id)
                return CustomError(Common.Status.BadRequest, "شناسه کاربر با شناسه کاربر احراز هویت شده متفاوت است");


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
                return CustomError(Common.Status.NotAuthenticated, "اجازه دسترسی ندارید");

            var isAdmin = HttpContext.User.IsInRole("admin");

            if(!isAdmin)
            {

                var userId = HttpContext.User.Identity.GetUserId();

                if (string.IsNullOrEmpty(userId))
                    return CustomError(Common.Status.NotAuthenticated, "اجازه دسترسی ندارید");


                var convertResult = Guid.TryParse(userId, out Guid id);

                if (!convertResult)
                    return CustomError(Common.Status.NotAuthenticated, "اجازه دسترسی ندارید");

                if (id != model.id)
                    return CustomError(Common.Status.BadRequest, "شناسه کاربر با شناسه کاربر احراز هویت شده متفاوت است");
            }

            if (model.current_password == model.new_password)
                    return CustomError(Common.Status.BadRequest, "لطفا از رمز عبور جدید استفاده نمایید");


            var command = new ChangePasswordCommand(model.id, model.user_name, model.current_password, model.new_password);

            var result = await _mediator.Send(command);

            return CustomOk(result);
        }

    }
}
