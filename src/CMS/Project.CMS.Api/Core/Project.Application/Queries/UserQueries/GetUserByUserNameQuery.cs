using System;
using MediatR;
using Project.Application.Dtos.UserDtos;

namespace Project.Application.Queries.UserQueries
{
    public class GetUserByUserNameQuery : IRequest<UserDto>
    {
        public GetUserByUserNameQuery(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; set; }
    }
}
