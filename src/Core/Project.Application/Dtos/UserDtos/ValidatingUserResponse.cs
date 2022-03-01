using System;
namespace Project.Application.Dtos.UserDtos
{
    public class ValidatingUserResponse
    {
        public ValidatingUserResponse(Guid id, string userName, string email, string phoneNumber, string[] roles, bool isActive)
        {
            Id = id;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            Roles = roles;
            IsActive = isActive;
        }


        public Guid Id { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public bool IsActive { get; private set; }
        public string[] Roles { get; private set; }
    }
}
