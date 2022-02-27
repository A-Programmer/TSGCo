using System;
namespace Project.Application.Dtos.RoleDtos
{
    public class EditRoleResponse
    {
        public EditRoleResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
