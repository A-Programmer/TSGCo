namespace Project.Auth.Areas.Admin.ViewModels.Clients
{
    public class AddPostLogoutRedirectUriViewModel
    {
        public int ClientId { get; set; }
        public string PostLogoutRedirectUri { get; set; }
    }
}