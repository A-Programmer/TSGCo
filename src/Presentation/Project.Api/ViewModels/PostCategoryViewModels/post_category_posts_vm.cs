using System;
using System.Collections.Generic;
using System.Linq;
using Project.Api.ViewModels.PostViewModels;
using Project.Api.ViewModels.UserViewModels;
using Project.Application.Dtos.Posts;
using Project.Api.ViewModels.PostKeywordViewModels;

namespace Project.Api.ViewModels.PostCategoryViewModels
{
    public class post_category_posts_vm
    {
        public post_category_posts_vm(Guid id, string title, string slug, string description, List<PostDto> posts)
        {
            this.id = id;
            this.title = title;
            this.slug = slug;
            this.description = description;
            this.posts = posts.Select(x => new post_vm(x.Id, x.Title, x.Slug, x.Description, x.Content, x.ImageUrl, x.Status, x.CreatedAt, x.ModifiedAt,
                x.VotesCount, x.ViewsCount, x.SeoTitle, x.SeoDescription, x.ShowInSlide)).ToList();
        }

        public Guid id { get; set; }
        public string title { get; set; }
        public string slug { get; set; }
        public string description { get; set; }
        public List<post_vm> posts { get; set; }
    }
}
