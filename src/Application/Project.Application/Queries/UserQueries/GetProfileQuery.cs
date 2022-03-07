using System;
using MediatR;
using Project.Application.Dtos.UserDtos;

namespace Project.Application.Queries.UserQueries
{
    public class GetProfileQuery : IRequest<GetUserProfileDto>
    {
        public GetProfileQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
