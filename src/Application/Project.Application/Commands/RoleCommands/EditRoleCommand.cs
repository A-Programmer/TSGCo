using System;
using MediatR;
using Project.Application.Dtos.RoleDtos;

namespace Project.Application.Commands.RoleCommands
{
    public class EditRoleCommand : IRequest<EditRoleResponse>
    {
        public EditRoleCommand(Guid id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
    }
}
