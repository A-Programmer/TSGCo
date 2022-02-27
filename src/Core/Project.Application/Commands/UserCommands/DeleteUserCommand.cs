using System;
using MediatR;
using Project.Application.Dtos.UserDtos;

namespace Project.Application.Commands.UserCommands
{
    public class DeleteUserCommand : IRequest<DeleteUserResponse>
    {
        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
