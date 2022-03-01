using System;
namespace Project.Api.ViewModels.RoleViewModels
{
    public class role_vm
    {
        public role_vm(Guid id, string name, string description)
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }

        public Guid id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
