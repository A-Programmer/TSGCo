using System;
namespace Project.Application.Dtos.MenuDtos
{
    public class AddMenuResponse
    {
        public AddMenuResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
