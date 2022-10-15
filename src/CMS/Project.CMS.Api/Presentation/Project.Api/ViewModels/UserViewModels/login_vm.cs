using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModels.UserViewModels
{
    public class login_vm
    {
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        public string grant_type { get; set; }
        public string refresh_token { get; set; }
        public string scope { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }

    }
}
