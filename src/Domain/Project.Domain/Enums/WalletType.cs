using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Enums
{
    public enum WalletType
    {
        [Display(Name = "کیف پول داخلی")]
        Internal,
        [Display(Name = "کیف پول خارجی")]
        External
    }
}
