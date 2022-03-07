using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Application.Commands.UserCommands;
using Project.Application.Dtos.UserDtos;
using Project.Application.Queries.UserQueries;
using Project.Application.Services.JwtServices;
using Project.Domain.Shared;
using Project.Domain.Shared.Exceptions;
using Project.Domain;

namespace Project.Application.Handlers.UserHandlers
{
    public class GetUserByRefreshTokenHandler : IRequestHandler<GetUserByRefreshTokenQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByRefreshTokenHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> Handle(GetUserByRefreshTokenQuery request, CancellationToken cancellationToken)
        {

            var user = await _unitOfWork.Users.GetUserByRefreshToken(request.RefreshToken);

            if (user == null)
                return null;

            return new UserDto(user.Id, user.UserName, user.Email, user.PhoneNumber, user.RegisteredAt, user.IsActive);

        }
    }
}
