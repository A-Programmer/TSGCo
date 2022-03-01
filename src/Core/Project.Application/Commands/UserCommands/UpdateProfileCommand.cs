using System;
using MediatR;
using Project.Common.Utilities;

namespace Project.Application.Commands.UserCommands
{
    public class UpdateProfileCommand : IRequest<Guid>
    {
        public UpdateProfileCommand(Guid id, string userName, string email, string phoneNumber, string firstName, string lastName, string profileImageUrl, string aboutMe, string birthDate)
        {
            Id = id;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            ProfileImageUrl = profileImageUrl;
            AboutMe = aboutMe;
            if(!string.IsNullOrEmpty(birthDate))
                BirthDate = birthDate.ToDateTimeOffset();
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public Guid Id { get; private set; }
        public string UserName { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string ProfileImageUrl { get; private set; }
        public string AboutMe { get; private set; }
        public DateTimeOffset? BirthDate { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
    }
}
