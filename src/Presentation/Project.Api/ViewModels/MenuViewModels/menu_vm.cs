using System;
using System.Collections.Generic;

namespace Project.Api.ViewModels.MenuViewModels
{
    public class menu_vm
    {
        public menu_vm(Guid id, string title, string url, int order, Guid? parentId)
        {
            this.id = id;
            this.title = title;
            this.url = url;
            this.order = order;
            this.parent_id = parentId;
        }


        public Guid id { get; set; }

        public string title { get; set; }

        public string url { get; set; }

        public int order { get; set; }

        public Guid? parent_id { get; set; }

    }
}
