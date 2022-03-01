using System;
namespace Project.Application.Dtos.DynamicPageDtos
{
    public class AddDynamicPageResponse
    {
        public AddDynamicPageResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
