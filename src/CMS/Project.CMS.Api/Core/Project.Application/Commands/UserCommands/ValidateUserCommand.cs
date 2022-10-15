using System;
using MediatR;
using Project.Application.Dtos.UserDtos;
using Project.Domain.Common;

namespace Project.Application.Commands.UserCommands
{
    public class ValidateUserCommand : IRequest<ValidatingUserResponse>
    {
        public ValidateUserCommand(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public string UserName { get; private set; }
        public string Password { get; private set; }
    }
}
