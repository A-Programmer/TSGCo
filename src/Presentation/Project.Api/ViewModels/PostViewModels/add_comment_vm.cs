using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModels.PostViewModels
{
    public class add_comment_vm
    {
        [Required]
        public string email { get; set; }
        public string full_name { get; set; }
        [Required]
        public string content { get; set; }
        public Guid? parent_id { get; set; }
    }
}
