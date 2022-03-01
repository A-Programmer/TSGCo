using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModels.UserViewModels
{
    public class add_user_vm
    {

        [Required]
        public string user_name { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string email { get; set; }

        public string phone_number { get; set; }

        public string[] role_names { get; set; }
    }
}
