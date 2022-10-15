using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModels.UserViewModels
{
    public class edit_user_vm
    {


        [Required]
        public string email { get; set; }

        public string phone_number { get; set; }

        public bool status { get; set; }

        public string[] role_names { get; set; }
    }
}
