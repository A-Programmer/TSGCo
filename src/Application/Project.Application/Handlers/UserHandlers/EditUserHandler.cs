using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Application.Commands.UserCommands;
using Project.Application.Dtos.UserDtos;
using Project.Domain.Shared;
using Project.Domain.Shared.Exceptions;
using Project.Domain.Shared.Utilities;
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

            user.Update(request.Email, request.PhoneNumber, new UserSecurityStamp(SecurityHelper.GenerateToken(), DateTimeOffset.Now, DateTimeOffset.Now.AddDays(2)));
            user.ChangeStatus(request.Status);

            user.UpdateUserRoles(user.Roles.Select(x => x.RoleId).ToArray());

            await _unitOfWork.CommitAsync();

            return new EditUserResponse(request.Id);
        }
    }
}
