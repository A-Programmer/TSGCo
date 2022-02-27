using System;
namespace Project.Application.Dtos.MenuDtos
{
    public class DeleteMenuResponse
    {
        public DeleteMenuResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
