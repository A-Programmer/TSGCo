using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Auth.ViewModels.UserRegistration
{
    public class RegisterUserViewModel
    {
        // credentials
        [MaxLength(100)]
        public string Username { get; set; }

        [MaxLength(100)]
        public string Password { get; set; }


        // claims
        [Required]
        [MaxLength(100)]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(100)]
        public string Lastname { get; set; }

        [Required]
        [MaxLength(200)]
        public string Address { get; set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; set; }
    }
}
