using System;
using System.Collections.Generic;
using System.Linq;
using Project.Application.Dtos.Posts;
using Project.Domain.Shared.Utilities;
using Project.Domain.Models.PostEntities;

namespace Project.Application.Dtos.PostCategoryDtos
{
    public class PostCategoryPostsDto
    {
        public PostCategoryPostsDto(Guid id, string title, string slug, string description, PaginatedList<PostDto> posts)
        {
            Id = id;
            Title = title;
            Slug = slug;
            Description = description;
            Posts = posts;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string Description { get; private set; }
        public PaginatedList<PostDto> Posts { get; private set; }
    }
}
