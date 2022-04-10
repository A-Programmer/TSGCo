using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.Api.ViewModels.UserViewModels;
using Project.WebFrameworks.Api;
using Project.Application.Commands.UserCommands;
using Project.Application.Queries.UserQueries;
using Project.Application.Dtos.UserDtos;
using Project.Common;
using Microsoft.AspNetCore.Authorization;
using Project.Api.ViewModels;
using Project.Common.Utilities;
using Project.Domain.Enums;


namespace Project.Api.Controllers
{
    [ApiVersion("1")]
    public class usersController : BaseController
    {
        private readonly IMediator _mediator;

        public usersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "admin")]
        [HttpGet(Routes.Users.Get.GetAllUsers)]
        public async Task<ActionResult<List<user_vm>>> Get(int? pageNumber, int? pageSize)
        {
            var query = new GetAllUsersQuery(pageNumber, pageSize);
            var users = await _mediator.Send(query);

            var result = users.Select(x => new user_vm(x.Id, x.UserName, x.Email, x.PhoneNumber, x.RegisteredDate, x.IsActive, x.Roles));

            return CustomPagedOk(result, users.PageIndex, users.TotalPages, users.TotalItems, users.ShowPagination);
        }

        [Authorize(Roles = "admin")]
        [HttpGet(Routes.Users.Get.GetUserById)]
        public async Task<ActionResult> Get(Guid id)
        {
            var query = new GetUserQuery(id);

            var result = await _mediator.Send(query);

            if (result == null)
                return CustomError(Common.Status.NotFound, "کاربر مورد نظر یافت نشد");

            var user = new user_vm(result.Id, result.UserName, result.Email, result.PhoneNumber, result.RegisteredDate, result.IsActive, result.Roles);

            return CustomOk(user);
        }

        [Authorize(Roles = "admin")]
        [HttpPost(Routes.Users.Post.AddUser)]
        public async Task<ActionResult> Post(add_user_vm model)
        {
            var command = new AddUserCommand(model.user_name, model.password, model.email, model.phone_number, model.role_names);

            var result = await _mediator.Send(command);

            return CustomOk(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<ActionResult> Put(Guid id, edit_user_vm model)
        {

            var getUserQuery = new GetUserQuery(id);
            var getUserQueryResult = await _mediator.Send(getUserQuery);

            if (getUserQueryResult == null)
                return CustomError(Status.NotFound, "کاربر مورد نظر یافت نشد");



            var command = new EditUserCommand(id, model.email, model.status, model.phone_number, model.role_names);

            var result = await _mediator.Send(command);

            return CustomOk(result);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<ActionResult> Delete(DeleteVm model)
        {
            var command = new DeleteUserCommand(model.id);

            var result = await _mediator.Send(command);

            return CustomOk(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPost(Routes.Users.Post.AddOrUpdateProfile)]
        public async Task<ActionResult> AddOrUpdateProfile(Guid user_id, add_or_update_profile model)
        {
            var addOrUpdateProfileCommand = new AddOrUpdateUserProfileCommand(user_id, model.first_name, model.last_name, model.profile_image_url, model.aboute_me, model.birth_date.ToDateTimeOffset());

            var result = await _mediator.Send(addOrUpdateProfileCommand);

            return CustomOk(result);
        }



    }
}
