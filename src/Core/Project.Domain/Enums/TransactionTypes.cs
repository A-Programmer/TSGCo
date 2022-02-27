using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Enums
{
    public enum TransactionTypes
    {
        [Display(Name = "واریز")]
        Deposit,

        [Display(Name = "برداشت")]
        Withdraw
    }
}
