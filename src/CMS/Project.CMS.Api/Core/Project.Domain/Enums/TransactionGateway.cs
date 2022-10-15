using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Enums
{
    public enum TransactionGateways
    {
        [Display( Name = "درگاه بانکی")]
        Bank,
        [Display( Name = "کیف پول داخلی")]
        Internal,
        [Display( Name = "کیف پول خارجی")]
        External
    }
}