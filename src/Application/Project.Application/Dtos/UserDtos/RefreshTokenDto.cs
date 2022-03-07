using System;
namespace Project.Application.Dtos.UserDtos
{
    public class RefreshTokenDto
    {
        public RefreshTokenDto(Guid userId, string name, string value)
        {
            UserId = userId;
            Name = name;
            Value = value;
        }

        public Guid UserId { get; private set; }
        public string Name { get; private set; }
        public string Value { get; private set; }
    }
}
