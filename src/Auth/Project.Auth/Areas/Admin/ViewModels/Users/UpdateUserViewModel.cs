using System.ComponentModel.DataAnnotations;

namespace Project.Auth.Areas.Admin.ViewModels.Users
{
    public class UpdateUserViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "ایمیل"), DataType(DataType.EmailAddress), Required(ErrorMessage = "ایمیل الزامی است")]
        public string Email { get; set; }

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