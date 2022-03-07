using System;

namespace Project.BlazorApp.Models
{
    public class MenuItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int AppearanceOrder { get; set; }
        public Guid? ParentId { get; set; }

    }
}

