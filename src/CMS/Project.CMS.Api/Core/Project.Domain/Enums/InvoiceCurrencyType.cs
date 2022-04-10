using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Enums
{
    public enum InvoiceCurrencyType
    {
        [Display(Name = "تومانی")]
        Toman,

        [Display(Name = "ارز دیجیتال")]
        CryptoCurrency,

        [Display(Name = "انتقال ارز پرفکت مانی")]
        PerfectMoneyTransfer,

        [Display(Name = "ووچر پرفکت مانی")]
        PerfectMoneyVoucher
    }
}
