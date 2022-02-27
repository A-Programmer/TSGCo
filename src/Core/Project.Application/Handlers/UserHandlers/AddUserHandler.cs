using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Application.Commands.UserCommands;
using Project.Application.Dtos.UserDtos;
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

            var hashedPassword = SecurityHelper.GetSha256Hash(request.Password);

            var user = new User(request.UserName, hashedPassword, request.Email, request.PhoneNumber, false);

            await _unitOfWork.Users.AddAsync(user);

            user.UpdateUserRoles(request.RolesIds);
        
            await _unitOfWork.CommitAsync();

            return new AddUserResponse(user.Id);
        }
    }
}
