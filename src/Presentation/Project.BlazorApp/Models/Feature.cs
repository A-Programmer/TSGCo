

namespace Project.BlazorApp.Models
{
    public class Feature
    {
        public Guid Id { get; set; }
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int AppearanceOrder { get; set; }
    }
}