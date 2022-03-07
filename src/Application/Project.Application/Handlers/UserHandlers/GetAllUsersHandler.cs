using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Application.Dtos.UserDtos;
using Project.Application.Queries.UserQueries;
using Project.Domain.Shared;
using Project.Domain.Shared.Exceptions;
using Project.Domain.Shared.Utilities;
using Project.Domain;

namespace Project.Application.Handlers.UserHandlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, PaginatedList<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUsersHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedList<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = (await _unitOfWork.Users.GetAllUsersWithRoles());
            if (!users.Any())
                throw new AppException(ApiResultStatusCode.NotFound, "کاربر مورد نظر یافت نشد", HttpStatusCode.NotFound);

            var result = users.Select(x => new UserDto(x.Id, x.UserName, x.Email, x.PhoneNumber, x.RegisteredAt, x.IsActive))
                .AsQueryable();
            return PaginatedList<UserDto>.Create(result, request.PageIndex, request.PageSize);
        }
    }
}
