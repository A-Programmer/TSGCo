using System;
namespace Project.Application.Dtos.PublicDtos.PostPublicDtos
{
    public class PublicPostsListDto
    {
        public PublicPostsListDto(Guid id, string title, string slug, string description, string imageUrl, bool status,
            int votesCount, int viewsCount, int commentsCount, string authorName, string[] categories, string[] keywords, bool showInSlide, DateTimeOffset createdDate, DateTimeOffset modifiedDate)
        {
            Id = id;
            Title = title;
            Slug = slug;
            Description = description;
            ImageUrl = imageUrl;
            Status = status;
            ShowInSlide = showInSlide;
            VotesCount = votesCount;
            ViewsCount = viewsCount;
            CommentsCount = commentsCount;
            AuthorName = authorName;
            Categories = categories;
            Keywords = keywords;
            CreatedAt = createdDate;
            ModifiedAt = modifiedDate;

        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ImageUrl { get; private set; }
        public bool Status { get; private set; }
        public string Slug { get; private set; }
        public int VotesCount { get; private set; }
        public bool ShowInSlide { get; private set; }
        public int ViewsCount { get; private set; }
        public int CommentsCount { get; private set; }
        public string AuthorName { get; private set; }
        public string[] Categories { get; private set; }
        public string[] Keywords { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset ModifiedAt { get; private set; }
    }
}
