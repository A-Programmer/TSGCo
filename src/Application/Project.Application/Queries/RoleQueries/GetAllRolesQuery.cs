using System;
using System.Collections.Generic;
using MediatR;
using Project.Application.Dtos.RoleDtos;

namespace Project.Application.Queries.RoleQueries
{
    public class GetAllRolesQuery : IRequest<List<RoleDto>>
    {
        public GetAllRolesQuery()
        {
        }
    }
}
