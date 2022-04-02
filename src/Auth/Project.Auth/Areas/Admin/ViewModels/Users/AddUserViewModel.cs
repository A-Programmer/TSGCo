using System.ComponentModel.DataAnnotations;

namespace Project.Auth.Areas.Admin.ViewModels
{
    public class AddUserViewModel
    {
        [Display(Name = "ایمیل"), DataType(DataType.EmailAddress), Required(ErrorMessage = "ایمیل الزامی است")]
        public string Email { get; set; }

        [Display(Name = "کلمه عبور"), DataType(DataType.Password), Required(ErrorMessage = "کلمه عبور الزامی است.")]
        public string Password { get; set; }
        
        [Display(Name = "تکرار کلمه عبور"), DataType(DataType.Password), Compare(nameof(Password)), Required(ErrorMessage = "کلمه عبور و تکرار آن یکسان نمی‌باشد.")]
        public string PasswordRepeat { get; set; }

        [Display(Name = "شماره موبایل"), DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "نام")]
        public string? FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string? LastName { get; set; }

        [Display(Name = "وضعیت فعال بودن کاربر")]
        public bool IsActive { get; set; }

    }
}