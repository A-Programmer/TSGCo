using System;
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
using Project.Domain.Models.UserEntities;

namespace Project.Application.Handlers.UserHandlers
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, RegisterResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RegisterHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {

            var hashedPassword = SecurityHelper.GetSha256Hash(request.Password);

            var user = new User(request.UserName, hashedPassword, request.Email, request.PhoneNumber, request.IsActive);

            await _unitOfWork.Users.AddAsync(user);

            await _unitOfWork.CommitAsync();

            return new RegisterResponse(user.Id);
        }
    }
}
