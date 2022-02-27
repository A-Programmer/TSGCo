using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModels.PostCategoryViewModels
{
    public class add_post_category_vm
    {
        [Required]
        public string title { get; set; }

        public string description { get; set; }
    }
}
