using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Enums
{
    public enum InvoiceStatus
    {
        [Display(Name = "ثبت شده")]
        Created,

        [Display(Name = "در حال انجام")]
        Processing,

        [Display(Name = "پرداخت شده")]
        Paied,

        [Display(Name = "آنجام شده")]
        Done,

        [Display(Name = "خطا")]
        Error
    }
}
