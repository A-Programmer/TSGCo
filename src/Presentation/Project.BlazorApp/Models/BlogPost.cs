
namespace Project.BlazorApp.Models
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public string Slug { get; set; }
        public string Category { get; set; }
    }
}