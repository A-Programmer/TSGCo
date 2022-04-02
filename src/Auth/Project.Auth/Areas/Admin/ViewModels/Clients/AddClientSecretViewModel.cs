using IdentityServer4;

namespace Project.Auth.Areas.Admin.ViewModels.Clients
{
    public class AddClientSecretViewModel
    {
        public int ClientId { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime? Expiration { get; set; }
        public string Type { get; set; }
    }
}