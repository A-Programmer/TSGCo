using System;
using MediatR;
using Project.Application.Dtos.UserDtos;

namespace Project.Application.Queries.UserQueries
{
    public class GetUserQuery : IRequest<UserDto>
    {
        public GetUserQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
