using System;
namespace Project.Application.Dtos.PostCategoryDtos
{
    public class DeletePostCategoryResponse
    {
        public DeletePostCategoryResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
