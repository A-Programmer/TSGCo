using System;
using System.IdentityModel.Tokens.Jwt;

namespace Project.Application.Dtos.UserDtos
{
    public class AccessTokenDto
    {
        public AccessTokenDto(JwtSecurityToken securityToken, string refreshToken)
        {
            Access_Token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            Refresh_Token = refreshToken;
            Token_Type = "Bearer";
            Expires_In = (int)(securityToken.ValidTo - DateTime.UtcNow).TotalSeconds;
        }
        public string Access_Token { get; private set; }
        public string Refresh_Token { get; private set; }
        public string Token_Type { get; private set; }
        public int Expires_In { get; private set; }
    }
}
