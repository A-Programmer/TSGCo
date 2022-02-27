using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModels.ContactUsViewModels
{
    public class add_contact_us_vm
    {
        public add_contact_us_vm(string fullName, string email, string title, string content)
        {

            this.full_name = fullName;
            this.email = email;
            this.title = title;
            this.content = content;
        }

        [Required]
        public string full_name { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public string content { get; set; }
    }
}
