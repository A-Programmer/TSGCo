using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Api.ViewModels.SlideViewModels
{
    public class add_slide_vm
    {
        public int view_order { get; set; }
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
