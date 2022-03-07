
namespace Project.BlazorApp.Models
{
    public class Certification
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ThumbUrl { get; set; }
        public string OriginalUrl { get; set; }
        public string Description { get; set; }
        public int AppearanceOrder { get; set; }
    }
}