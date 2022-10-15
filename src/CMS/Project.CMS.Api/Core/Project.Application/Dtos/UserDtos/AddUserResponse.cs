using System;
namespace Project.Application.Dtos.UserDtos
{
    public class AddUserResponse
    {
        public AddUserResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
