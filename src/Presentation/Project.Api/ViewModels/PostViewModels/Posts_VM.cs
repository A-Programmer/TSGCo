using System;
using System.Collections.Generic;

namespace Project.Api.ViewModels.PostViewModels
{
    public class Posts_VM
    {
        public Posts_VM(Guid id, string title, string slug, string description, string imageUrl, bool status,
            DateTimeOffset createdAt, DateTimeOffset modifiedAt, string authorName,
            int votes_count, int views_count, List<string> categories, List<string> keywords)
        {
            this.Id = id;
            this.AuthorName = authorName;
            this.Title = title;
            this.Slug = slug;
            this.Description = description;
            this.ImageUrl = imageUrl;
            this.Status = status;
            this.CreatedAt = createdAt;
            this.ModifiedAt = modifiedAt;
            this.Categories = categories.ToArray();
            this.Keywords = keywords.ToArray();


            this.VotesCount = votes_count;
            this.ViewsCount = views_count;
        }

        public Guid Id { get; private set; }
        public string AuthorName { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ImageUrl { get; private set; }
        public bool Status { get; private set; }
        public string Slug { get; private set; }
        public int ViewsCount { get; private set; }
        public int VotesCount { get; private set; }
        public string[] Categories { get; private set; }
        public string[] Keywords { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public DateTimeOffset ModifiedAt { get; private set; }
    }
}
