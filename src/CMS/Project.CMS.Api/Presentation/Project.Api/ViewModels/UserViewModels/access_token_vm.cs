using System;
namespace Project.Api.ViewModels.UserViewModels
{
    public class access_token_vm
    {
        public access_token_vm(string access_token, string refresh_token, string token_type, int expires_in)
        {
            this.access_token = access_token;
            this.refresh_token = refresh_token;
            this.token_type = token_type;
            this.expires_in = expires_in;
        }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
}
