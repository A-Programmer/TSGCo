using System;
using MediatR;

namespace Project.Application.Commands.UserCommands
{
    public class ChangePasswordCommand : IRequest<Guid>
    {
        public ChangePasswordCommand(Guid id, string userName, string currentPassword, string newPassword)
        {
            Id = id;
            UserName = userName;
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }



        public Guid Id { get; private set; }
        
        public string UserName { get; private set; }
        
        public string CurrentPassword { get; private set; }
        
        public string NewPassword { get; private set; }
    }
}
