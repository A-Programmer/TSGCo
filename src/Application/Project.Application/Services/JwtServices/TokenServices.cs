using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Project.Domain;
using Project.Domain.Common;
using Project.Domain.Models.RoleEntities;
using Project.Domain.Models.UserEntities;

namespace Project.Application.Services.JwtServices
{
    public class TokenServices
    {

        public JwtSecurityToken GenerateToken(User user, List<Role> userRoles, JwtOptions options)
        {
            var secretKey = Encoding.UTF8.GetBytes(options.SecretKey);

            var signInCredentials =
                new SigningCredentials(new SymmetricSecurityKey(secretKey),
                                        SecurityAlgorithms.HmacSha256Signature);

            var secretKey2 = Encoding.UTF8.GetBytes(options.SecretKey2);

            var encryptionCredential =
                new EncryptingCredentials(new SymmetricSecurityKey(secretKey2),
                                        SecurityAlgorithms.HmacSha256Signature);

            var claims = _getClaims(user, userRoles);

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = options.Issuer,
                Audience = options.Audience,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow.AddMinutes(options.NotBeforeInMinutes),
                Expires = DateTime.UtcNow.AddMinutes(options.ExpirationInMinutes),
                SigningCredentials = signInCredentials,
                //EncryptingCredentials = encryptionCredential,
                Subject = new ClaimsIdentity(claims),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(descriptor);

            //var jwt = tokenHandler.WriteToken(token);

            return token;
        }


        private IEnumerable<Claim> _getClaims(User user, List<Role> userRoles)
        {
            var securityStampClaimType = new ClaimsIdentityOptions().SecurityStampClaimType;

            var latestActiveSecurityStampObject = user.GetLatestActiveSecurityStamp();

            var latestActiveSecurityStamp = "";

            if (latestActiveSecurityStampObject != null)
                latestActiveSecurityStamp = latestActiveSecurityStampObject.SecurityStamp;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(securityStampClaimType, latestActiveSecurityStamp)
            };



            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name.ToLower()));
            }

            return claims;
        }

    }
}
