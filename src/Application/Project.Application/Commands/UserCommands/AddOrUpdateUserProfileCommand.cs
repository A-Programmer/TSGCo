using System;
using MediatR;
using Project.Application.Dtos.UserDtos;

namespace Project.Application.Commands.UserCommands
{
    public class AddOrUpdateUserProfileCommand : IRequest<AddOrUpdateUserProfileResponse>
    {
        public AddOrUpdateUserProfileCommand(Guid userId, string firstName, string lastName, string profileImageUrl, string aboutMe, DateTimeOffset birthDate)
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            ProfileImageUrl = profileImageUrl;
            AboutMe = aboutMe;
            BirthDate = birthDate;
        }
        public Guid UserId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string ProfileImageUrl { get; private set; }
        public string AboutMe { get; private set; }
        public DateTimeOffset BirthDate { get; private set; }
    }
}
