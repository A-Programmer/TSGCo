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
using Project.Domain.Models.RoleEntities;
using Project.Domain.Models.UserEntities;

namespace Project.Application.Handlers.UserHandlers
{
    public class EditUserHandler : IRequestHandler<EditUserCommand, EditUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public EditUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EditUserResponse> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetUserWithRolesById(request.Id);

            if (user == null)
                throw new AppException(ApiResultStatusCode.NotFound, "کاربر یافت نشد", HttpStatusCode.NotFound);

            if (!request.Email.IsValidEmail())
                throw new AppException(ApiResultStatusCode.BadRequest, "ایمیل صحیح نمی باشد", HttpStatusCode.BadRequest);

            if (!request.PhoneNumber.IsValidMobile())
                throw new AppException(ApiResultStatusCode.BadRequest, "تلفن صحیح نمی باشد", HttpStatusCode.BadRequest);


            user.Update(request.Email, request.PhoneNumber, new UserSecurityStamp(SecurityHelper.GenerateToken(), DateTimeOffset.Now, DateTimeOffset.Now.AddDays(2)));
            user.ChangeStatus(request.Status);

            if(request.RoleNames.Any())
            {
                if(user.Roles.Any())
                {
                    foreach (var role in user.Roles.ToList())
                    {
                        user.RemoveUserRole(role);
                    }
                }

                foreach (var roleString in request.RoleNames)
                {
                    var role = await _unitOfWork.Roles.GetRoleByName(roleString);

                    if (role == null)
                        throw new AppException(ApiResultStatusCode.NotFound, "نقش وارد شده صحیح نمیباشد", System.Net.HttpStatusCode.NotFound);
                    else
                        user.AddUserToRole(role);
                }
            }

            await _unitOfWork.CommitAsync();

            return new EditUserResponse(request.Id);
        }
    }
}
