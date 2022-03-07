using System;
using System.Collections.Generic;
using System.Linq;
using Project.Application.Dtos.Posts;

namespace Project.Application.Dtos.PostCategoryDtos
{
    public class PostCategoryDto
    {
        public PostCategoryDto(Guid id, string title, string slug, string description)
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
