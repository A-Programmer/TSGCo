
namespace Project.BlazorApp.Models
{
    public class NewsLetter
    {
        public Guid Id { get; set; }
        public string IpAddress { get; set; }
        public string Email { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
}