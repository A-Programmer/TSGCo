using System;

namespace Project.BlazorApp.Models
{
    public class Slide
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string PrimaryButtonText { get; set; }
        public string PrimaryButtonUrl { get; set; }
        public string SecondaryButtonText { get; set; }
        public string SecondaryButtonUrl { get; set; }
        public int AppearanceOrder { get; set; }
    }
}