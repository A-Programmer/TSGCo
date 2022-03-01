using System;
namespace Project.Application.Dtos.DynamicPageDtos
{
    public class EditDynamicPageResponse
    {
        public EditDynamicPageResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
