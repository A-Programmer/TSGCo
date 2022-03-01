using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModels.AnnouncementViewModels
{
    public class edit_announcement_vm
    {
        public edit_announcement_vm(Guid id, string title, string description, string content)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.content = content;
        }

        [Required]
        public Guid id { get; set; }
        [Required]
        public string title { get; set; }
        public string description { get; set; }
        [Required]
        public string content { get; set; }
    }
}
