using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModels.MenuViewModels
{
    public class edit_menu_vm
    {
        public edit_menu_vm(Guid id, string title, string url, int order, Guid? parentId)
        {
            this.id = id;
            this.title = title;
            this.url = url;
            this.order = order;
            this.parent_id = parentId;
        }

        [Required]
        public Guid id { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public string url { get; set; }

        [Required]
        public int order { get; set; }

        public Guid? parent_id { get; set; }
    }
}
