using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Api.ViewModels;
using Project.Api.ViewModels.RoleViewModels;
using Project.WebFrameworks.Api;
using Project.Application.Commands.RoleCommands;
using Project.Application.Queries.RoleQueries;


namespace Project.Api.Controllers
{
    [ApiVersion("1")]
    public class rolesController : BaseController
    {
        private readonly IMediator _mediator;
        public rolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "admin")]
        [HttpGet(Routes.Roles.Get.GetAll)]
        public async Task<ActionResult<List<role_vm>>> Get()
        {
            var query = new GetAllRolesQuery();
            var users = await _mediator.Send(query);

            var result = users.Select(x => new role_vm(x.Id, x.Title, x.Description));

            return CustomOk(result);
        }

        [Authorize(Roles = "admin")]
        [HttpGet(Routes.Roles.Get.GetById)]
        public async Task<ActionResult> Get(Guid id)
        {
            var query = new GetRoleQuery(id);

            var result = await _mediator.Send(query);

            if (result == null)
                return CustomError(Common.Status.NotFound, "نقش مورد نظر یافت نشد");

            return CustomOk(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPost(Routes.Roles.Post.Add)]
        public async Task<ActionResult> Post(add_role_vm model)
        {
            var command = new AddRoleCommand(model.name, model.description);

            var result = await _mediator.Send(command);

            return CustomOk(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<ActionResult> Put(Guid id, edit_role_vm model)
        {
            var command = new EditRoleCommand(id, model.name, model.description);

            var result = await _mediator.Send(command);

            return CustomOk(result);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<ActionResult> Delete(DeleteVm model)
        {
            var command = new DeleteRoleCommand(model.id);

            var result = await _mediator.Send(command);

            return CustomOk(result);
        }
    }
}