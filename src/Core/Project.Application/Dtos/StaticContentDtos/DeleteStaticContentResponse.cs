using System;
namespace Project.Application.Dtos.StaticContentDtos
{
    public class DeleteStaticContentResponse
    {
        public DeleteStaticContentResponse(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
    }
}
