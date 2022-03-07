using System;
using MediatR;
using Project.Application.Dtos.RoleDtos;

namespace Project.Application.Queries.RoleQueries
{
    public class GetRoleQuery : IRequest<RoleDto>
    {
        public GetRoleQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
