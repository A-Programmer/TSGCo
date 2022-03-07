using System;
namespace Project.Application.Dtos.DynamicPageDtos
{
    public class DeleteDynamicPageResponse
    {
        public DeleteDynamicPageResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
