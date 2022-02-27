using System;
namespace Project.Api.ViewModels.DashboardViewModels
{
    public class user_dashboard_profile_vm
    {
        public user_dashboard_profile_vm(Guid user_id, string user_name, string email, string phone_number, string first_name, string last_name, string full_name, string about_me, string birth_date, string[] roles, string profile_image_url)
        {
            this.user_id = user_id;
            this.user_name = user_name;
            this.email = email;
            this.phone_number = phone_number;
            this.first_name = first_name;
            this.last_name = last_name;
            this.full_name = full_name;
            this.about_me = about_me;
            this.birth_date = birth_date;
            this.roles = roles;
            this.profile_image_url = profile_image_url;
        }

        public Guid user_id { get; set; }
        public string user_name { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string full_name { get; set; }
        public string about_me { get; set; }
        public string birth_date { get; set; }
        public string[] roles { get; set; }
        public string profile_image_url { get; set; }
    }
}
