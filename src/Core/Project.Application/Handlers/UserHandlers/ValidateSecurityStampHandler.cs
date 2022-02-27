using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Application.Commands.UserCommands;
using Project.Domain;

namespace Project.Application.Handlers.UserHandlers
{
    public class ValidateSecurityStampHandler : IRequestHandler<ValidateSecurityStampCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValidateSecurityStampHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(ValidateSecurityStampCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetUserBySecurityStamp(request.SecurityStamp);

            return user != null;
        }
    }
}
