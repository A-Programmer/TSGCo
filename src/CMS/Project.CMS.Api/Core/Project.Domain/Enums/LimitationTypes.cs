using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Enums
{
    public enum LimitationTypes
    {
        [Display(Name = "کمترین")]
        Min,

        [Display(Name = "بیشترین")]
        Max
    }
}
