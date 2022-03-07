using System;
using MediatR;
using Project.Application.Dtos.RoleDtos;

namespace Project.Application.Commands.RoleCommands
{
    public class AddRoleCommand : IRequest<AddRoleResponse>
    {
        public AddRoleCommand(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
    }
}
