using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Api.ViewModels.SlideViewModels
{
    public class edit_slide_vm
    {
        [Required]
        public Guid id { get; set; }
        [Required]
        public int view_order { get; set; }
        [Required]
        public string image_url { get; set; }
        [Required]
        public string title { get; set; }
        public string description { get; set; }
        public string buttun_text { get; set; }
        public string buttun_url { get; set; }
        [Required]
        public bool status { get; set; }
    }
}
