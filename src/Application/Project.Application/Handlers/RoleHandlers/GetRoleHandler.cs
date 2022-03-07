using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Project.Application.Dtos.RoleDtos;
using Project.Application.Queries.RoleQueries;


namespace Project.Application.Handlers.RoleHandlers
{
    public class GetRoleHandler : IRequestHandler<GetRoleQuery, RoleDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRoleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RoleDto> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var role = await _unitOfWork.Roles.GetByIdAsync(request.Id);
            return new RoleDto(role.Id, role.Name, role.Description);
        }
    }
}
