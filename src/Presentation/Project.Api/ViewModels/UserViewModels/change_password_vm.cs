using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModels.UserViewModels
{
    public class change_password_vm
    {
        public change_password_vm(Guid id, string user_name, string current_password, string new_password, string new_password_repeat)
        {
            this.id = id;
            this.user_name = user_name;
            this.current_password = current_password;
            this.new_password = new_password;
            this.new_password_repeat = new_password_repeat;
        }

        [Required(ErrorMessage = "شناسه کاربر الزامی است")]
        public Guid id { get; set; }
        [Required(ErrorMessage = "نام کاربری الزامی است")]
        public string user_name { get; set; }
        [Required(ErrorMessage = "کلمه عبور فعلی الزامی است")]
        public string current_password { get; set; }
        [Required(ErrorMessage = "کلمه عبور جدید الزامی است")]
        public string new_password { get; set; }
        [Required(ErrorMessage = "تکرار کلمه عبور جدید الزامی است")]
        [Compare(nameof(new_password), ErrorMessage = "کلمه عبور جدید با تکرار آن یکی نیست")]
        public string new_password_repeat { get; set; }
    }
}
