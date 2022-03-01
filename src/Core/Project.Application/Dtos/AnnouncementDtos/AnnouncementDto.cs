using System;
namespace Project.Application.Dtos.AnnouncementDtos
{
    public class AnnouncementDto
    {
        public AnnouncementDto(Guid id, string title, string slug, string description, string content, string createdDate, string modifiedDate)
        {
            Id = id;
            Title = title;
            Slug = slug;
            Description = description;
            Content = content;
            CreatedDate = createdDate;
            ModifiedDate = modifiedDate;
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string Description { get; private set; }
        public string Content { get; private set; }
        public string CreatedDate { get; private set; }
        public string ModifiedDate { get; private set; }
    }
}
