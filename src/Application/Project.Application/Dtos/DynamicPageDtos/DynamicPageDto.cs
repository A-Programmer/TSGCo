using System;
namespace Project.Application.Dtos.DynamicPageDtos
{
    public class DynamicPageDto
    {
        public DynamicPageDto(Guid id, string title, string description, string content, string imageUrl, string createdAt, string modifiedAt)
        {
            Id = id;
            Title = title;
            Description = description;
            Content = content;
            ImageUrl = imageUrl;
            CreatedAt = createdAt;
            ModifiedAt = modifiedAt;
        }
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string Description { get; private set; }
        public string Content { get; private set; }
        public string ImageUrl { get; private set; }
        public string CreatedAt { get; private set; }
        public string ModifiedAt { get; private set; }
    }
}
