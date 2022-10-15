using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Application.Dtos.RoleDtos;
using Project.Application.Queries.RoleQueries;
using Project.Domain;

namespace Project.Application.Handlers.RoleHandlers
{
    public class GetRoleByNameHandler : IRequestHandler<GetRoleByNameQuery, RoleDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRoleByNameHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RoleDto> Handle(GetRoleByNameQuery request, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork.Roles.GetRoleByName(request.RoleName);

            if (role == null)
                return null;

            return new RoleDto(role.Id, role.Name, role.Description);
        }
    }
}
