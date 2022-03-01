using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Enums
{
    public enum UnitTypes
    {

        [Display(Name = "تومان")]
        Toman,

        [Display(Name = "ارز دیجیتال")]
        CryptoCurrency,

        [Display(Name = "پرفکت مانی")]
        PerfectMoney
    }
}
