using System;

namespace Project.Application.Dtos.UserDtos
{
    public class UserDto
    {

        public UserDto(Guid id, string userName, string email, string phoneNumber, DateTimeOffset registeredDate, bool isActive, string[] roles = null)
        {
            Id = id;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            RegisteredDate = registeredDate;
            Roles = roles;
            IsActive = isActive;
        }

        public Guid Id { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public bool IsActive { get; private set; }
        public DateTimeOffset RegisteredDate { get; private set; }
        public string[] Roles { get; private set; }
    }
}
