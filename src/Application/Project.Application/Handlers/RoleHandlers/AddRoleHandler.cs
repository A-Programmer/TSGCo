using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Application.Commands.RoleCommands;
using Project.Application.Dtos.RoleDtos;
using Project.Domain;
using Project.Domain.Models.RoleEntities;

namespace Project.Application.Handlers.RoleHandlers
{
    public class AddRoleHandler : IRequestHandler<AddRoleCommand, AddRoleResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddRoleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AddRoleResponse> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var role = new Role(request.Title, request.Description);
            await _unitOfWork.Roles.AddAsync(role);
            await _unitOfWork.CommitAsync();
            return new AddRoleResponse(role.Id);
        }
    }
}
