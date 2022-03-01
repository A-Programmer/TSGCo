using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Application.Commands.UserCommands;
using Project.Common;
using Project.Common.Exceptions;
using Project.Domain;

namespace Project.Application.Handlers.UserHandlers
{
    public class AddUserToRoleHandler : IRequestHandler<AddUserToRoleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddUserToRoleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(AddUserToRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(request.UserId);

            if (user == null)
                throw new AppException(ApiResultStatusCode.NotFound, "User does not exist", System.Net.HttpStatusCode.NotFound);

            user.UpdateUserRoles(request.RolesIds);


            var result = await _unitOfWork.CommitAsync();

            return (result == 1);
        }
    }
}
