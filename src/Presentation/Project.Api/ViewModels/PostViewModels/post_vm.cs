using System;

namespace Project.Api.ViewModels.PostViewModels
{
    public class post_vm
    {
        public post_vm(Guid id, string title, string slug, string description, string content, string imageUrl, bool status, DateTimeOffset createdAt, DateTimeOffset modifiedAt,
            int votes_count, int views_count, string seo_title, string seo_description, bool show_in_slides)
        {
            this.id = id;
            this.title = title;
            this.slug = slug;
            this.description = description;
            this.content = content;
            this.image_url = imageUrl;
            this.status = status;
            this.show_in_slides = show_in_slides;
            this.created_at = createdAt;
            this.modified_at = modifiedAt;

            this.seo_title = seo_title;
            this.seo_description = seo_description;

            this.votes_count = votes_count;
            this.views_count = views_count;
        }

        public Guid id { get; private set; }
        public string title { get; private set; }
        public string seo_title { get; private set; }
        public string content { get; private set; }
        public string description { get; private set; }
        public string seo_description { get; private set; }
        public string image_url { get; private set; }
        public bool status { get; private set; }
        public bool show_in_slides { get; set; }
        public string slug { get; private set; }
        public int views_count { get; private set; }
        public int votes_count { get; private set; }
        public DateTimeOffset created_at { get; private set; }
        public DateTimeOffset modified_at { get; private set; }
    }
}
