using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Application.Commands.UserCommands;
using Project.Application.Dtos.UserDtos;
using Project.Common;
using Project.Common.Exceptions;
using Project.Common.Utilities;
using Project.Domain;

namespace Project.Application.Handlers.UserHandlers
{
    public class ValidateUserHandler : IRequestHandler<ValidateUserCommand, ValidatingUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValidateUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ValidatingUserResponse> Handle(ValidateUserCommand request, CancellationToken cancellationToken)
        {
            var hashedPassword = SecurityHelper.GetSha256Hash(request.Password);
            var user = await _unitOfWork.Users.GetUserByUserNameAndPassword(request.UserName, hashedPassword);

            if (user == null)
                user = await _unitOfWork.Users.GetUserByEmailAndPassword(request.UserName, hashedPassword);

            if (user == null)
                user = await _unitOfWork.Users.GetUserByMobileAndPassword(request.UserName, hashedPassword);

            if (user == null)
                throw new AppException(ApiResultStatusCode.NotFound, "کاربر یافت نشد", HttpStatusCode.NotFound);

            string[] roles = user.Roles.Select(x => x.Name).ToArray();

            return new ValidatingUserResponse(user.Id, user.UserName, user.Email, user.PhoneNumber, roles, user.IsActive);
        }

    }
}
