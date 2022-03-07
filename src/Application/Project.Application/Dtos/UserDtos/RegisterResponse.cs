using System;
namespace Project.Application.Dtos.UserDtos
{
    public class RegisterResponse
    {
        public RegisterResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
