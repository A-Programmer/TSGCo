using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModels.UserViewModels
{
    public class register_vm
    {
        [Required(ErrorMessage = "نام کاربری الزامی است")]
        public string user_name { get; set; }

        [Required(ErrorMessage = "کلمه عبور الزامی است")]
        public string password { get; set; }

        [Required(ErrorMessage = "ایمیل الزامی است")]
        [EmailAddress]
        public string email { get; set; }

        [Required(ErrorMessage = "شماره موبایل الزامی است")]
        public string phone_number { get; set; }
    }
}
