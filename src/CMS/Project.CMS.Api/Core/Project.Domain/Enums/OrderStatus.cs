using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "ثبت شده")]
        Created,

        [Display(Name = "تایید شده")]
        Confirmed,

        [Display(Name = "انجام شده")]
        Done,

        [Display(Name = "خطا")]
        Error,

        [Display(Name = "مرحله اول موفق")]
        FirstPartDone,

        [Display(Name = "لغو شده")]
        Canceled,
    }
}
