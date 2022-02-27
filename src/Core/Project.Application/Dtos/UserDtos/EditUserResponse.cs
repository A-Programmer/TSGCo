using System;
namespace Project.Application.Dtos.UserDtos
{
    public class EditUserResponse
    {
        public EditUserResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
