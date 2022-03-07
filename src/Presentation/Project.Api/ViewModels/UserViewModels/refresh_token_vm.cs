using System;
namespace Project.Api.ViewModels.UserViewModels
{
    public class refresh_token_vm
    {
        public refresh_token_vm(string refresh_Token)
        {
            refresh_token = refresh_Token;
        }

        public string refresh_token { get; set; }
    }
}
