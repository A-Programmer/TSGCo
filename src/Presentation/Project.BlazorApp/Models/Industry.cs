
namespace Project.BlazorApp.Models
{
    public class Industry
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Icon { get; set; }
        public int AppearanceOrder { get; set; }
    }
}