using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Enums
{
    public enum BaseCurrencyTypes
    {
        [Display(Name = "تومان")]
        Toman,
        [Display(Name = "دلار تتر")]
        Tether,
        [Display(Name = "پرفکت مانی")]
        PerfectMoney
    }
}