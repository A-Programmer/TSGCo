using System;
using System.Collections.Generic;
using System.Linq;
using Project.Api.ViewModels.UserViewModels;
using Project.Application.Dtos.DashboardDtos;

namespace Project.Api.ViewModels.DashboardViewModels
{
    public class user_dashboard
    {
        public user_dashboard(UserDashboardProfileDto profile, List<UserDashboardLoginDateDto> login_dates, List<UserDashboardTicketDto> tickets)
        {
            if (profile != null)
                this.profile = new user_dashboard_profile_vm(profile.Id, profile.UserName, profile.Email, profile.PhoneNumber,
                    profile.FirstName, profile.LastName, profile.FullName, profile.AboutMe, profile.BirthDate, profile.Roles, profile.ProfileImageUrl);
            if (login_dates.Any())
                this.login_dates = login_dates.Select(x => new user_dashboard_login_dates_vm(x.LoginDate)).ToList();
            if (tickets != null)
                this.tickets = tickets?.Select(x => new user_dashboard_ticket_vm(x.Id, x.Title, x.CreatedDate, x.CategoryTitle, x.Status)).ToList();
        }

        public user_dashboard_profile_vm profile { get; private set; }
        public List<user_dashboard_login_dates_vm> login_dates { get; private set; }
        public List<user_dashboard_ticket_vm> tickets { get; private set; }
    }
}
