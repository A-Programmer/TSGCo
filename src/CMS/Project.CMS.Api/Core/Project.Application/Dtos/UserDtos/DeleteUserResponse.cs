using System;
namespace Project.Application.Dtos.UserDtos
{
    public class DeleteUserResponse
    {
        public DeleteUserResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
