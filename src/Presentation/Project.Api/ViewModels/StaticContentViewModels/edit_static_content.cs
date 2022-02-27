using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModels.StaticContentViewModels
{
    public class edit_static_content
    {

        [Required]
        public Guid id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string content { get; set; }
    }
}
