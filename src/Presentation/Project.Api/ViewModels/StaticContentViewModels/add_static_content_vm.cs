using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModels.StaticContentViewModels
{
    public class add_static_content_vm
    {

        [Required]
        public string title { get; set; }
        [Required]
        public string content { get; set; }
    }
}
