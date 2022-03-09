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
    public class signupController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly PublicSettings _settings;
        public signupController(IMediator mediator,
            IOptionsSnapshot<PublicSettings> optionsSnapshot)
        {
            _mediator = mediator;
            _settings = optionsSnapshot.Value;
        }

        [HttpPost(Routes.Register.SignUp)]
        public async Task<IActionResult> Login()
        {
            return Ok("Not Implemented");
        }
    }
}