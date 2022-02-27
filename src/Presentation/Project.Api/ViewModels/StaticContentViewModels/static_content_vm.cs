using System;
namespace Project.Api.ViewModels.StaticContentViewModels
{
    public class static_content_vm
    {
        public static_content_vm(Guid id, string title, string content)
        {
            this.id = id;
            this.title = title;
            this.content = content;
        }

        public Guid id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
    }
}
