using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModels.PostCategoryViewModels
{
    public class edit_post_category_vm
    {
        [Required]
        public Guid id { get; set; }
        [Required]
        public string title { get; set; }
        public string description { get; set; }
    }
}
