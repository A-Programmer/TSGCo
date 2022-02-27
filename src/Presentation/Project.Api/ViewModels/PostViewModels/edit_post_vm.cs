using System;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModels.PostViewModels
{
    public class edit_post_vm
    {
        [Required]
        public Guid id { get; set; }
        [Required]
        public string title { get; set; }
        public string seo_title { get; set; }
        [Required]
        public string content { get; set; }
        [Required]
        public string description { get; set; }
        public string seo_description { get; set; }
        [Required]
        public string image_url { get; set; }
        [Required]
        public bool status { get; set; }
        public bool show_in_slides { get; set; }
        [Required]
        public string[] categories { get; set; }
        public string[] keywords { get; set; }
    }
}
