using System;
namespace Project.Application.Dtos.StaticContentDtos
{
    public class StaticContentDto
    {
        public StaticContentDto(Guid id, string title, string content)
        {
            Id = id;
            Title = title;
            Content = content;
        }
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
    }
}
