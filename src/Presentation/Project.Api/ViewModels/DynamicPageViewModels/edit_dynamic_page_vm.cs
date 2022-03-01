using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModels.DynamicPageViewModels
{
    public class edit_dynamic_page_vm
    {
        public edit_dynamic_page_vm(Guid id, string title, string description, string content, string imageUrl)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.content = content;
            this.image_url = imageUrl;
        }

        public Guid id { get; set; }
        [Required]
        public string title { get; set; }
        public string description { get; set; }
        [Required]
        public string content { get; set; }
        public string image_url { get; set; }
    }
}
