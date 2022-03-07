using System;
namespace Project.Application.Dtos.RoleDtos
{
    public class AddRoleResponse
    {
        public AddRoleResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
