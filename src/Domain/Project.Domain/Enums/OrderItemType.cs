using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Enums
{
    public enum OrderItemType
    {
        [Display(Name = "خرید")]
        Buy,
        [Display(Name = "فروش")]
        Sell
    }
}
