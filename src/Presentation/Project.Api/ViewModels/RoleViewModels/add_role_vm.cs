using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModels.RoleViewModels
{
    public class add_role_vm
    {
        public add_role_vm(string name, string description)
        {
            this.name = name;
            this.description = description;
        }

        [Required]
        public string name { get; set; }
        public string description { get; set; }
    }
}
