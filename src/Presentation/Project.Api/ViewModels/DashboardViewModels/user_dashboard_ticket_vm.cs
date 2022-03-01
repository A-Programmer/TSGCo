using System;
using Project.Common.Utilities;

namespace Project.Api.ViewModels.DashboardViewModels
{
    public class user_dashboard_ticket_vm
    {
        public user_dashboard_ticket_vm(Guid id, string title, string created_date, string category_title, string status)
        {
            this.id = id;
            this.title = title;
            this.created_date = created_date;
            this.category_title = category_title;
            this.status = status;
        }

        public Guid id { get; private set; }
        public string title { get; private set; }
        public string created_date { get; private set; }
        public string category_title { get; private set; }
        public string status { get; private set; }
    }
}
