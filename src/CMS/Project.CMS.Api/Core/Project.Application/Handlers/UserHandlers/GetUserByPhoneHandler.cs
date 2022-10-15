using System;
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
    public class GetUserByPhoneHandler : IRequestHandler<GetUserByPhoneQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByPhoneHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> Handle(GetUserByPhoneQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetUserByPhone(request.PhoneNumber);

            if (user == null)
                return null;

            return new UserDto(user.Id, user.UserName, user.Email, user.PhoneNumber, user.RegisteredAt, user.IsActive);
        }
    }
}
