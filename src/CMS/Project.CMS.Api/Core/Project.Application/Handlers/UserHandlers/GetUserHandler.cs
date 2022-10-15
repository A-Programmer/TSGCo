using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Application.Dtos.UserDtos;
using Project.Application.Queries.UserQueries;
using Project.Common;
using Project.Common.Exceptions;
using Project.Domain;

namespace Project.Application.Handlers.UserHandlers
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetUserWithRolesById(request.Id);

            if (user != null)
            {
                var roles = user.Roles.Select(x => x.Name).ToArray();
                return new UserDto(user.Id, user.UserName, user.Email, user.PhoneNumber, user.RegisteredAt, roles, user.IsActive);
            }
            else
                return null;
            
        }
    }
}
