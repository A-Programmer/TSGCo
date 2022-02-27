using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Application.Dtos.UserDtos;
using Project.Application.Queries.UserQueries;
using Project.Domain;

namespace Project.Application.Handlers.UserHandlers
{
    public class GetUserByUserNameHandler : IRequestHandler<GetUserByUserNameQuery, UserDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserByUserNameHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> Handle(GetUserByUserNameQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetUserByName(request.UserName);

            if (user == null)
                return null;

            return new UserDto(user.Id, user.UserName, user.Email, user.PhoneNumber, user.RegisteredAt, user.IsActive);
        }
    }
}
