using System;
using MediatR;

namespace Project.Application.Commands.UserCommands
{
    public class AddUserToRoleCommand : IRequest<bool>
    {
        public AddUserToRoleCommand(Guid userId, Guid[] rolesIds)
        {
            UserId = userId;
            RolesIds = rolesIds;
        }

        public Guid UserId { get; private set; }
        public Guid[] RolesIds { get; private set; }
    }
}
