using System;
using Project.Domain.Shared.Utilities;

namespace Project.Application.Dtos.DashboardDtos
{
    public class UserDashboardLoginDateDto
    {
        public UserDashboardLoginDateDto(DateTimeOffset loginDate)
        {
            LoginDate = loginDate.ToPersianDateTime();
        }

        public string LoginDate { get; private set; }
    }
}
