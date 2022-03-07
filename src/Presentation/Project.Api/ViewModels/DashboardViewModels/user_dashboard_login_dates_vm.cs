using System;

namespace Project.Api.ViewModels.DashboardViewModels
{
    public class user_dashboard_login_dates_vm
    {
        public user_dashboard_login_dates_vm(string login_date)
        {
            this.login_date = login_date;
        }

        public string login_date { get; private set; }
    }
}
