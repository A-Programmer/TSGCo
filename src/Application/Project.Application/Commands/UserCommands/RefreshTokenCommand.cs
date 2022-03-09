using System;
using MediatR;
using Project.Application.Dtos.UserDtos;
using Project.Domain.Shared;

namespace Project.Application.Commands.UserCommands
{
    public class RefreshTokenCommand : IRequest<AccessTokenDto>
    {
        public RefreshTokenCommand(string refreshToken, JwtOptions options)
        {
            RefreshToken = refreshToken;
            JwtOptions = options;
        }

        public string RefreshToken { get; private set; }
        public JwtOptions JwtOptions { get; private set; }
    }
}
