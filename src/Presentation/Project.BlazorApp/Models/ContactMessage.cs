
using System.ComponentModel.DataAnnotations;

namespace Project.BlazorApp.Models
{
    public class ContactMessage
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Message text is required.")]
        public string Message { get; set; }
    }
}