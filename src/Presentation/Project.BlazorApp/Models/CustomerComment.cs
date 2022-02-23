
namespace Project.BlazorApp.Models
{
    public class CustomerComment
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
        public string Comment { get; set; }
        public string ImageUrl { get; set; }
        public int AppearanceOrder { get; set; }
    }
}