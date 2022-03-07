using System;
using MediatR;
using Project.Application.Dtos.UserDtos;

namespace Project.Application.Queries.UserQueries
{
    public class GetUserByRefreshTokenQuery : IRequest<UserDto>
    {
        public GetUserByRefreshTokenQuery(string refreshToken)
        {
            RefreshToken = refreshToken;
        }

        public string RefreshToken { get; private set; }
    }
}
