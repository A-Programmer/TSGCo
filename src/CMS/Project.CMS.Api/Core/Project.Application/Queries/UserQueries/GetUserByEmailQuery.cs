using System;
using MediatR;
using Project.Application.Dtos.UserDtos;

namespace Project.Application.Queries.UserQueries
{
    public class GetUserByEmailQuery : IRequest<UserDto>
    {
        public GetUserByEmailQuery(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}
