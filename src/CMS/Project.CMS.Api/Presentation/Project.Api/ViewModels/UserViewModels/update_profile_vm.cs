using System;
using Project.Common.Utilities;

namespace Project.Api.ViewModels.UserViewModels
{
    public class update_profile_vm
    {

        public Guid user_id { get; set; }
        public string user_name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string profile_image_url { get; set; }
        public string about_me { get; set; }
        public string birth_date { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
    }
}
