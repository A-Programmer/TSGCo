using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Enums
{
    public enum VerificationType
    {
        [Display(Name = "موبایل")]
        Mobile,

        [Display(Name = "تلفن")]
        Phone,

        [Display(Name = "کد ملی")]
        InternationalId,

        [Display(Name = "کارت بانگی")]
        CreditCardNumber,

        [Display(Name = "شبا")]
        Sheba,

        [Display(Name = "تصویر رضایت نامه")]
        Agreement
    }
}
