using System;
using Project.Common.Utilities;

namespace Project.Api.ViewModels.AnnouncementViewModels
{
    public class announcement_vm
    {
        public announcement_vm(Guid id, string title, string slug, string description, string content, string createdDate, string modifiedDate)
        {
            this.id = id;
            this.title = title;
            this.slug = slug;
            this.description = description;
            this.content = content;
            this.created_date = createdDate;
            this.modified_date = modifiedDate;
        }

        public Guid id { get; private set; }
        public string title { get; private set; }
        public string slug { get; private set; }
        public string description { get; private set; }
        public string content { get; private set; }
        public string created_date { get; private set; }
        public string modified_date { get; private set; }
    }
}
