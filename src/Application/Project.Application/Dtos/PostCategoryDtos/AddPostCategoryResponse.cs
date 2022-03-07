using System;
namespace Project.Application.Dtos.PostCategoryDtos
{
    public class AddPostCategoryResponse
    {
        public AddPostCategoryResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
