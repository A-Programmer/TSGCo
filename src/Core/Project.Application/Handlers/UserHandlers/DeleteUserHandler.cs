using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Project.Application.Commands.UserCommands;
using Project.Application.Dtos.UserDtos;
using Project.Common;
using Project.Common.Exceptions;
using Project.Domain;

namespace Project.Application.Handlers.UserHandlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, DeleteUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _unitOfWork.Users.GetUserWithRelationsById(request.Id);

            if (user.Posts.Any())
                throw new AppException(ApiResultStatusCode.ServerError, "ابتدا پست های این کاربر را حذف کنید", System.Net.HttpStatusCode.InternalServerError);

            foreach (var token in user.UserTokens)
                user.RemoveToken(token);

            user.UpdateUserRoles(null);

            _unitOfWork.Users.Remove(user);

            await _unitOfWork.CommitAsync();

            return new DeleteUserResponse(request.Id);
        }

    }
}
