using System;

namespace Project.Application.Dtos.Posts
{
    public class PostDto
    {
        public PostDto(Guid id, string title, string slug, string description, string content, string imageUrl, bool status,
            int votesCount, int viewsCount, string seoTitle, string seoDescription, bool showInSlide, DateTimeOffset createdDate, DateTimeOffset modifiedDate)
        {
            Id = id;
            Title = title;
            SeoTitle = seoTitle;
            Slug = slug;
            Description = description;
            SeoDescription = seoDescription;
            Content = content;
            ImageUrl = imageUrl;
            Status = status;
            ShowInSlide = showInSlide;
            VotesCount = votesCount;
            ViewsCount = viewsCount;
            CreatedAt = createdDate;
            ModifiedAt = modifiedDate;

        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string SeoTitle { get; private set; }
        public string Content { get; private set; }
        public string SeoDescription { get; private set; }
        public string Description { get; private set; }
        public string ImageUrl { get; private set; }
        public bool Status { get; private set; }
        public string Slug { get; private set; }
        public int VotesCount { get; private set; }
        public bool ShowInSlide { get; private set; }
        public int ViewsCount { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset ModifiedAt { get; private set; }
    }
}
