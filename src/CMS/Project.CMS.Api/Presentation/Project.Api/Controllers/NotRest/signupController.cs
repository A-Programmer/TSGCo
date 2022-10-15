using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Api.ViewModels.UserViewModels;
using Project.Application.Commands.RoleCommands;
using Project.Application.Commands.UserCommands;
using Project.Application.Queries.RoleQueries;
using Project.Application.Queries.UserQueries;
using Project.Common;
using Project.Common.Exceptions;
using Project.Common.Utilities;
using Project.WebFrameworks.Api;

namespace Project.Api.Controllers
{
    [ApiVersion("1")]
    public class signupController : BaseController
    {
        private readonly IMediator _mediator;

        public signupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Routes.Registration.Register)]
        public async Task<ActionResult> Register(register_vm model)
        {
            var existingByEmailQuery = new GetUserByEmailQuery(model.email);
            var existingByEmailResult = await _mediator.Send(existingByEmailQuery);
            if (existingByEmailResult != null)
                return CustomError(Common.Status.DuplicateRecord, "ایمیل تکراری است.");


            var existingByUserNameQuery = new GetUserByUserNameQuery(model.user_name);
            var existingByUserNameResult = await _mediator.Send(existingByUserNameQuery);
            if (existingByUserNameResult != null)
                return CustomError(Common.Status.DuplicateRecord, "نام کاربری تکراری است.");


            var existingByPhoneQuery = new GetUserByPhoneQuery(model.phone_number);
            var existingByPhoneResult = await _mediator.Send(existingByPhoneQuery);
            if (existingByPhoneResult != null)
                return CustomError(Common.Status.DuplicateRecord, "شماره تماس تکراری است.");


            if (!model.email.IsValidEmail())
                throw new AppException(ApiResultStatusCode.BadRequest, "ایمیل صحیح نمی باشد", HttpStatusCode.BadRequest);

            if (!model.phone_number.IsValidMobile())
                throw new AppException(ApiResultStatusCode.BadRequest, "شماره موبایل صحیح نمی باشد", HttpStatusCode.BadRequest);


            var registerCommand = new RegisterCommand(model.user_name, model.password, model.email, model.phone_number, true);
            var registerResult = await _mediator.Send(registerCommand);

            if (registerResult == null)
                return CustomError(Common.Status.ServerError, "خطای سرور رخ داده است");

            var roleQuery = new GetRoleByNameQuery("User");
            var roleResult = await _mediator.Send(roleQuery);

            if (roleResult == null)
            {
                var addRoleCommand = new AddRoleCommand("User", "Users");
                var addRoleResult = await _mediator.Send(addRoleCommand);

                var addToRoleCommand = new AddUserToRoleCommand(registerResult.Id, addRoleResult.Id);
                var addToRoleResult = await _mediator.Send(addToRoleCommand);
            }
            else
            {
                var addToRoleCommand = new AddUserToRoleCommand(registerResult.Id, roleResult.Id);
                var addToRoleResult = await _mediator.Send(addToRoleCommand);
            }

            //Insert admin role if does not exist
            var adminRoleQuery = new GetRoleByNameQuery("Admin");
            var adminRoleResult = await _mediator.Send(adminRoleQuery);

            if (adminRoleResult == null)
            {
                var addRoleCommand = new AddRoleCommand("Admin", "Admins");
                var addRoleResult = await _mediator.Send(addRoleCommand);
                if (model.email.ToLower() == "mrsadin@gmail.com")
                {
                    var addToRoleCommand = new AddUserToRoleCommand(registerResult.Id, addRoleResult.Id);
                    var addToRoleResult = await _mediator.Send(addToRoleCommand);
                }
            }
            else
            {
                if (model.email.ToLower() == "mrsadin@gmail.com")
                {
                    var addToRoleCommand = new AddUserToRoleCommand(registerResult.Id, adminRoleResult.Id);
                    var addToRoleResult = await _mediator.Send(addToRoleCommand);
                }
            }

            return CustomOk(registerResult);
        }

    }
}

