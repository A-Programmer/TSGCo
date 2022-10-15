using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Application.Commands.UserCommands;
using Project.Common;
using Project.Common.Exceptions;
using Project.Common.Utilities;
using Project.Domain;
using Project.Domain.Models.UserEntities;

namespace Project.Application.Handlers.UserHandlers
{
    public class UpdateSecurityStampHandler : IRequestHandler<UpdateSecurityStampCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSecurityStampHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateSecurityStampCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);

            if (user == null)
                throw new AppException(ApiResultStatusCode.NotFound, "کاربر مورد نظر یافت نشد.", HttpStatusCode.NotFound);

            user.UpdateSecurityStamp(new UserSecurityStamp(SecurityHelper.GenerateToken(), DateTimeOffset.Now, DateTimeOffset.Now.AddDays(7)));

            var result = await _unitOfWork.CommitAsync();

            return (result >= 1);
        }
    }
}
