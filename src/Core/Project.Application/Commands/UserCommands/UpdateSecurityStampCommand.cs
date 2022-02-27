using System;
using MediatR;
using Project.Domain.Models.UserEntities;

namespace Project.Application.Commands.UserCommands
{
    public class UpdateSecurityStampCommand : IRequest<bool>
    {
        public UpdateSecurityStampCommand(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; private set; }
    }
}
