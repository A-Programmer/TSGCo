using System;
namespace Project.Api.ViewModels.DynamicPageViewModels
{
    public class dynamic_page_vm
    {
        public dynamic_page_vm(Guid id, string title, string slug, string description, string content, string imageUrl)
        {
            this.id = id;
            this.title = title;
            this.slug = slug;
            this.description = description;
            this.content = content;
            this.image_url = imageUrl;
        }

        public Guid id { get; set; }
        public string title { get; set; }
        public string slug { get; set; }
        public string description { get; set; }
        public string content { get; set; }
        public string image_url { get; set; }
    }
}
