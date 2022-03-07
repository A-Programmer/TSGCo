using System;
using MediatR;

namespace Project.Application.Commands.UserCommands
{
    public class ValidateSecurityStampCommand : IRequest<bool>
    {
        public ValidateSecurityStampCommand(string securityStamp)
        {
            SecurityStamp = securityStamp;
        }

        public string SecurityStamp { get; private set; }
    }
}
