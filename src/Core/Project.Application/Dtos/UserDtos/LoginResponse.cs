using System;
namespace Project.Application.Dtos.UserDtos
{
    public class LoginResponse
    {
        public LoginResponse(string token)
        {
            Token = token;
        }

        public string Token { get; private set; }
    }
}
