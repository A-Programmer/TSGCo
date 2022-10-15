using System;
using MediatR;

namespace Project.Application.Commands.UserCommands
{
    public class AddUserToRoleCommand : IRequest<bool>
    {
        public AddUserToRoleCommand(Guid userId, Guid roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }

        public Guid UserId { get; private set; }
        public Guid RoleId { get; private set; }
    }
}
