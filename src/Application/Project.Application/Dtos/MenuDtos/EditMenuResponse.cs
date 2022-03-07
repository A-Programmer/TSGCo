using System;
namespace Project.Application.Dtos.MenuDtos
{
    public class EditMenuResponse
    {
        public EditMenuResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
