using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModels.AnnouncementViewModels
{
    public class add_Announcement_vm
    {
        public add_Announcement_vm(string title, string description, string content)
        {
            this.title = title;
            this.description = description;
            this.content = content;
        }

        [Required]
        public string title { get; set; }
        public string description { get; set; }
        [Required]
        public string content { get; set; }
    }
}
