using System;
namespace Project.Api.ViewModels.PostViewModels
{
    public class post_slide_vm
    {
        public post_slide_vm(Guid id, string title, string slug, string description, string imageUrl, string seo_title, string seo_description)
        {
            this.id = id;
            this.title = title;
            this.slug = slug;
            this.description = description;
            this.image_url = imageUrl;
            this.seo_title = seo_title;
            this.seo_description = seo_description;
        }

        public Guid id { get; private set; }
        public string title { get; private set; }
        public string seo_title { get; private set; }
        public string description { get; private set; }
        public string seo_description { get; private set; }
        public string image_url { get; private set; }
        public string slug { get; private set; }
    }
}
