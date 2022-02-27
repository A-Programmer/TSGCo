using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModels.MenuViewModels
{
    public class add_menu_vm
    {
        public add_menu_vm(string title, string url, int order, Guid? parentId = null)
        {
            this.title = title;
            this.url = url;
            this.order = order;
            this.parent_id = parentId;
        }



        [Required]
        public string title { get; set; }

        [Required]
        public string url { get; set; }

        [Required]
        public int order { get; set; }

        public Guid? parent_id { get; set; }
    }
}
