using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModels.RoleViewModels
{
    public class edit_role_vm
    {
        public edit_role_vm(Guid id, string name, string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }

        [Required]
        public Guid id { get; set; }
        [Required]
        public string name { get; set; }
        public string description { get; set; }
    }
}
