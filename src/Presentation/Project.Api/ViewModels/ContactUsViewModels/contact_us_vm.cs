using System;
namespace Project.Api.ViewModels.ContactUsViewModels
{
    public class contact_us_vm
    {
        public contact_us_vm(Guid id, string fullName, string email, string title, string content, string createdDate, string creatorIp)
        {
            this.id = id;
            this.full_name = fullName;
            this.email = email;
            this.title = title;
            this.content = content;
            this.created_date = createdDate;
            this.creator_ip = creatorIp;
        }

        public Guid id { get; private set; }
        public string full_name { get; private set; }
        public string email { get; private set; }
        public string title { get; private set; }
        public string content { get; private set; }
        public string created_date { get; private set; }
        public string creator_ip { get; private set; }
    }
}
