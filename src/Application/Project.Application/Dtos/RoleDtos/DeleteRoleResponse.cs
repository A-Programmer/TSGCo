using System;
namespace Project.Application.Dtos.RoleDtos
{
    public class DeleteRoleResponse
    {
        public DeleteRoleResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
