using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModels.DynamicPageViewModels
{
    public class add_dynamic_page_vm
    {
        public add_dynamic_page_vm(string title, string description, string content, string imageUrl)
        {
            this.title = title;
            this.description = description;
            this.content = content;
            this.image_url = imageUrl;
        }

        [Required]
        public string title { get; set; }
        public string description { get; set; }
        [Required]
        public string content { get; set; }
        public string image_url { get; set; }
    }
}
