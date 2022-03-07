using System;
using MediatR;
using Project.Application.Dtos.UserDtos;

namespace Project.Application.Commands.UserCommands
{
    public class RegisterCommand : IRequest<RegisterResponse>
    {
        public RegisterCommand(string userName, string passwrod, string email, string phoneNumber = "", bool isActive = false)
        {
            UserName = userName;
            Password = passwrod;
            Email = email;
            PhoneNumber = phoneNumber;
            IsActive = isActive;
        }

        public string UserName { get; private set; }

        public string Password { get; private set; }

        public string Email { get; private set; }

        public string PhoneNumber { get; private set; }

        public bool IsActive { get; private set; }
    }
}
