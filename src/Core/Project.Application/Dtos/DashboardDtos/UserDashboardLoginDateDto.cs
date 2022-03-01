using System;
using Project.Common.Utilities;

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
