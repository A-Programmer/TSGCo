using System;
namespace Project.Api.ViewModels.PostCategoryViewModels
{
    public class post_categories_list_vm
    {
        public post_categories_list_vm(Guid id, string title, string slug, string description)
        {
            this.id = id;
            this.title = title;
            this.slug = slug;
            this.description = description;
        }

        public Guid id { get; private set; }
        public string title { get; private set; }
        public string slug { get; private set; }
        public string description { get; private set; }
    }
}
