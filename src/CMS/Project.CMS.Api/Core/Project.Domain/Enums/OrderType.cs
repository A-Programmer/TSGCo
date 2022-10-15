using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Enums
{
    public enum OrderType
    {
        [Display(Name = "خرید")]
        Buy = 0,
        [Display(Name = "فروش")]
        Sell = 1,
        [Display(Name = "تبدیل")]
        Convert = 2,
        
    }
}
