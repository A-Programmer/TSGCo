using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Project.Api.ViewModels.UserViewModels;
using Project.Application.Commands.UserCommands;
using Project.Domain.Shared;
using Project.WebFrameworks.Api;

namespace Project.Api.Controllers
{
    [ApiVersion("1")]
    public class signinController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly PublicSettings _settings;
        public signinController(IMediator mediator,
            IOptionsSnapshot<PublicSettings> optionsSnapshot)
        {
            _mediator = mediator;
            _settings = optionsSnapshot.Value;
        }

        [HttpPost(Routes.Login.SignIn)]
        public async Task<IActionResult> Login([FromForm]login_vm model)
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
    }
}