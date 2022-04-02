namespace Project.Auth.Areas.Admin.ViewModels.Users
{
    public class UpdateUserProfileViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public Guid UserId { get; set; }
    }
}