using System;
namespace Project.Application.Dtos.Posts
{
    public class PostSlideDto
    {
        public PostSlideDto(Guid id, string title, string slug, string description, string imageUrl,
            string seoTitle, string seoDescription)
        {
            Id = id;
            Title = title;
            SeoTitle = seoTitle;
            Slug = slug;
            Description = description;
            SeoDescription = seoDescription;
            ImageUrl = imageUrl;
        }


        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string SeoTitle { get; private set; }
        public string SeoDescription { get; private set; }
        public string Description { get; private set; }
        public string ImageUrl { get; private set; }
        public string Slug { get; private set; }
    }
}
