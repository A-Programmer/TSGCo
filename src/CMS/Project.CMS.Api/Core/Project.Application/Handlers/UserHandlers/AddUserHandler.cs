using System;
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
using Project.Domain.Models.UserEntities;

namespace Project.Application.Handlers.UserHandlers
{
    public class AddUserHandler : IRequestHandler<AddUserCommand, AddUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AddUserResponse> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            if (!request.Email.IsValidEmail())
                throw new AppException(ApiResultStatusCode.BadRequest, "ایمیل صحیح نمی باشد", HttpStatusCode.BadRequest);

            if (!request.PhoneNumber.IsValidMobile())
                throw new AppException(ApiResultStatusCode.BadRequest, "تلفن صحیح نمی باشد", HttpStatusCode.BadRequest);


            var hashedPassword = SecurityHelper.GetSha256Hash(request.Password);

            var user = new User(request.UserName, hashedPassword, request.Email, request.PhoneNumber, false);

            await _unitOfWork.Users.AddAsync(user);

            foreach (var roleName in request.RoleNames)
            {
                var role = await _unitOfWork.Roles.GetRoleByName(roleName);

                if (role == null)
                    throw new AppException(ApiResultStatusCode.NotFound, "نقش وارد شده صحیح نمیباشد", System.Net.HttpStatusCode.NotFound);
                else
                    user.AddUserToRole(role);

            }

            await _unitOfWork.CommitAsync();

            return new AddUserResponse(user.Id);
        }
    }
}
