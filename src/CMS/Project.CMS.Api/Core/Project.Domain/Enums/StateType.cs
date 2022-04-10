using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Enums
{
    public enum PostStateType
    {
        [Display(Name = "Draft")]
        Draft,

        [Display(Name = "Accepted")]
        Accepted,

        [Display(Name = "Disabled")]
        Disabled
    }
}
