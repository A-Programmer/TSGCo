using System;
using Project.Application.Dtos.UserDtos;
using MediatR;

namespace Project.Application.Commands.UserCommands
{
    public class AddUserCommand : IRequest<AddUserResponse>
    {
        public AddUserCommand(string userName, string password, string email, string phoneNumber = "", Guid[] roles = null)
        {
            UserName = userName;
            Password = password;
            Email = email;
            PhoneNumber = phoneNumber;
            RolesIds = roles;
        }

        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public Guid[] RolesIds { get; private set; }
    }
}
