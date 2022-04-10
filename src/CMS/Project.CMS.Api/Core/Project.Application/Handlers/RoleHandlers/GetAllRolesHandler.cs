using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Application.Dtos.RoleDtos;
using Project.Application.Queries.RoleQueries;
using Project.Domain;

namespace Project.Application.Handlers.RoleHandlers
{
    public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, List<RoleDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllRolesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<RoleDto>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return (await _unitOfWork.Roles.GetAllAsync()).Select(x => new RoleDto(x.Id, x.Name, x.Description)).ToList();
        }
    }
}
