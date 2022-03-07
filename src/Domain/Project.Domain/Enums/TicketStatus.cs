using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Enums
{
    public enum TicketStatus
    {
        [Display(Name = "باز")]
        Open,

        [Display(Name = "پاسخ کاربر")]
        UserResponse,

        [Display(Name = "پاسخ ادمین")]
        SupportResponse,

        [Display(Name = "بسته")]
        Close
    }
}
