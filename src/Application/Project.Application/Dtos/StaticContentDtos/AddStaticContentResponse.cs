using System;
namespace Project.Application.Dtos.StaticContentDtos
{
    public class AddStaticContentResponse
    {
        public AddStaticContentResponse(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
    }
}
