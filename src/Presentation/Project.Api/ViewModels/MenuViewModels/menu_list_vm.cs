using System;
using System.Collections.Generic;

namespace Project.Api.ViewModels.MenuViewModels
{
    public class menu_list_vm
    {
        public menu_list_vm()
        {
            this.children = new List<menu_list_vm>();
        }
        public menu_vm menu { get; set; }
        public List<menu_list_vm> children { get; set; }
    }
}
