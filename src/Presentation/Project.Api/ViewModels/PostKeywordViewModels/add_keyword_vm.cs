using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModels.PostKeywordViewModels
{
    public class add_keyword_vm
    {
        [Required]
        public string title { get; set; }
    }
}
