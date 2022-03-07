using System;
namespace Project.Application.Dtos.ContactUsDtos
{
    public class ContactUsDto
    {
        public ContactUsDto(Guid id, string fullName, string email, string title, string content, string createdDate, string creatorIp)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            Title = title;
            Content = content;
            CreatedDate = createdDate;
            CreatorIp = creatorIp;
        }

        public Guid Id { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public string CreatedDate { get; private set; }
        public string CreatorIp { get; private set; }
    }
}
