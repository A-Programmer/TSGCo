using System;
using MediatR;

namespace Project.Application.Commands.UserCommands
{
    public class UpdateLastLoginDateCommand : IRequest<bool>
    {
        public UpdateLastLoginDateCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
