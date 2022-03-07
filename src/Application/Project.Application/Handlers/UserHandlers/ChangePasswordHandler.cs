using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Application.Commands.UserCommands;
using Project.Domain.Shared;
using Project.Domain.Shared.Exceptions;
using Project.Domain.Shared.Utilities;
using Project.Domain;
using Project.Domain.Models.UserEntities;

namespace Project.Application.Handlers.UserHandlers
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ChangePasswordHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {

            var user = await _unitOfWork.Users.GetUserByUserNameAndPassword(request.UserName, SecurityHelper.GetSha256Hash(request.CurrentPassword));

            if (user == null)
                throw new AppException(ApiResultStatusCode.NotFound, "کاربر مورد نظر یافت نشد", HttpStatusCode.NotFound);

            user.ChangePassword(SecurityHelper.GetSha256Hash(request.NewPassword), new UserSecurityStamp(SecurityHelper.GenerateToken(), DateTimeOffset.Now, DateTimeOffset.Now.AddDays(2)));

            var result = await _unitOfWork.CommitAsync();

            return user.Id;

        }
    }
}
