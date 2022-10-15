using System;
using MediatR;
using Project.Application.Dtos.RoleDtos;

namespace Project.Application.Queries.RoleQueries
{
    public class GetRoleByNameQuery : IRequest<RoleDto>
    {
        public GetRoleByNameQuery(string roleName)
        {
            RoleName = roleName;
        }

        public string RoleName { get; private set; }
    }
}
