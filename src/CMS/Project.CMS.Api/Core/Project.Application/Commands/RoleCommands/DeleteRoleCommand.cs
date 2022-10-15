using System;
using MediatR;
using Project.Application.Dtos.RoleDtos;

namespace Project.Application.Commands.RoleCommands
{
    public class DeleteRoleCommand : IRequest<DeleteRoleResponse>
    {
        public DeleteRoleCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
