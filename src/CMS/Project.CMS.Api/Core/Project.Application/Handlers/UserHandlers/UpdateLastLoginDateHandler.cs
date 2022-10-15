using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Application.Commands.UserCommands;
using Project.Common;
using Project.Common.Exceptions;
using Project.Domain;
using Project.Domain.Models.UserEntities;

namespace Project.Application.Handlers.UserHandlers
{
    public class UpdateLastLoginDateHandler : IRequestHandler<UpdateLastLoginDateCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLastLoginDateHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateLastLoginDateCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.Id);

            if (user == null)
                throw new AppException(ApiResultStatusCode.NotFound, "کاربر یافت نشد", HttpStatusCode.NotFound);

            user.UpdateLastLoginDate(new UserLoginDate(DateTimeOffset.Now));

            var result = await _unitOfWork.CommitAsync();

            return result == 1;
        }
    }
}
