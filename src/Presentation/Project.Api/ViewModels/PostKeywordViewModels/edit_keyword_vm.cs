using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModels.PostKeywordViewModels
{
    public class edit_keyword_vm
    {
        [Required]
        public Guid id { get; set; }

        [Required]
        public string title { get; set; }
    }
}
