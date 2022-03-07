using System;
using System.Collections.Generic;
using System.Linq;
using Project.Domain.Models.UserEntities;

namespace Project.Application.Dtos.DashboardDtos
{
    public class UserDashboardDto
    {
        public UserDashboardDto(User user)
        {
            var roles = user.Roles.Select(x => x.RoleId.ToString()).ToArray();
           Profile = new UserDashboardProfileDto(user.Id, user.UserName, user.Email, user.PhoneNumber,
               user.Profile?.FirstName, user.Profile?.LastName, user.Profile?.AboutMe, user.Profile?.AboutMe, roles, user.Profile?.ProfileImageUrl, user.Profile?.BirthDate);

            if(user.LoginDates.Any())
                LoginDates = user.LoginDates.Select(x => new UserDashboardLoginDateDto(x.LoginDate)).ToList();
        }

        public UserDashboardProfileDto Profile { get; private set; }
        public List<UserDashboardLoginDateDto> LoginDates { get; private set; }
        public List<UserDashboardTicketDto> Tickets { get; private set; }
    }
}
