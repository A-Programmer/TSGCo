using System;
namespace Project.Application.Dtos.UserDtos
{
    public class AddOrUpdateUserProfileResponse
    {
        public AddOrUpdateUserProfileResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
