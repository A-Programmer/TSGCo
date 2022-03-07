using System;
using MediatR;
using Project.Application.Dtos.UserDtos;

namespace Project.Application.Commands.UserCommands
{
    public class EditUserCommand : IRequest<EditUserResponse>
    {
        public EditUserCommand(Guid id, string userName, string email, bool status, string phoneNumber = "", string[] roles = null)
        {
            Id = id;
            UserName = userName;
            Email = email;
            Status = status;
            PhoneNumber = phoneNumber;
            RoleNames = roles;
        }

        public Guid Id { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public bool Status { get; private set; }
        public string[] RoleNames { get; private set; }
    }
}
