using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Project.Application.Commands.RoleCommands;
using Project.Application.Dtos.RoleDtos;
using Project.Domain.Shared.Exceptions;
using Project.Domain.Shared;

namespace Project.Application.Handlers.RoleHandlers
{
    public class EditRoleHandler : IRequestHandler<EditRoleCommand, EditRoleResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditRoleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EditRoleResponse> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {

            var role = await _unitOfWork.Roles.GetByIdAsync(request.Id);

            if (role == null)
                throw new AppException(ApiResultStatusCode.NotFound, "نقش مورد نظر یافت نشد", System.Net.HttpStatusCode.NotFound);

            role.Update(request.Title, request.Description);

            await _unitOfWork.CommitAsync();

            return new EditRoleResponse(request.Id);
        }
    }
}
