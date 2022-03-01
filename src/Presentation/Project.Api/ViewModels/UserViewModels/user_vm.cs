using System;

namespace Project.Api.ViewModels.UserViewModels
{
    public class user_vm
    {
        public user_vm(Guid id, string user_name, string email, string phone_number, DateTimeOffset register_date, bool status, string[] roles = null)
        {
            this.id = id;
            this.user_name = user_name;
            this.email = email;
            this.phone_number = phone_number;
            this.register_date = register_date;
            this.status = status;
            this.roles = roles;
        }


        public Guid id { get; private set; }
        public string user_name { get; private set; }
        public string email { get; private set; }
        public string phone_number { get; private set; }
        public DateTimeOffset register_date { get; private set; }
        public bool status { get; private set; }
        public string[] roles { get; private set; }
    }
}
