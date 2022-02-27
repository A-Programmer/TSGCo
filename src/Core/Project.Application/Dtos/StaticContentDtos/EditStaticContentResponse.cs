using System;
namespace Project.Application.Dtos.StaticContentDtos
{
    public class EditStaticContentResponse
    {
        public EditStaticContentResponse(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }
    }
}
