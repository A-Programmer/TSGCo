using System;
namespace Project.Application.Dtos.PostCategoryDtos
{
    public class EditPostCategoryResponse
    {
        public EditPostCategoryResponse(Guid id, string title, string slug, string description)
        {
            Id = id;
            Title = title;
            Slug = slug;
            Description = description;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string Description { get; private set; }
    }
}
