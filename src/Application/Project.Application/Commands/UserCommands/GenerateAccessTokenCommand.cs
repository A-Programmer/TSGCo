using System;
using MediatR;
using Project.Application.Dtos.UserDtos;
using Project.Domain.Common;

namespace Project.Application.Commands.UserCommands
{
    public class GenerateAccessTokenCommand : IRequest<AccessTokenDto>
    {
        public GenerateAccessTokenCommand(string userName, JwtOptions options)
        {
            UserName = userName;
            Options = options;
        }

        public string UserName { get; private set; }
        public JwtOptions Options { get; private set; }
    
    }
}
