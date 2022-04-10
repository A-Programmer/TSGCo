using System;
using Project.Common.Utilities;

namespace Project.Application.Dtos.UserDtos
{
    public class GetUserProfileDto
    {
        public GetUserProfileDto(Guid userId, string userName, string email, string phoneNumber, string firstName, string lastName, string fullName, string aboutMe, string[] roles, string profileImageUrl , DateTimeOffset? birthDate)
        {
            Id = userId;
            UserName = userName;
            Email = email;
            PhoneNumber = phoneNumber;
            FirstName = firstName;
            LastName = lastName;
            FullName = fullName;
            AboutMe = aboutMe;
            Roles = roles;
            ProfileImageUrl = profileImageUrl;
            if (birthDate.HasValue)
                BirthDate = birthDate.ToPersianDate();
        }

        public Guid Id { get; private set; }
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName { get; private set; }
        public string AboutMe { get; private set; }
        public string ProfileImageUrl { get; private set; }
        public string[] Roles { get; private set; }
        public string BirthDate { get; private set; }
    }
}
