using System;
using Project.Domain.Shared.Utilities;
using Project.Domain.Enums;

namespace Project.Application.Dtos.DashboardDtos
{
    public class UserDashboardTicketDto
    {
        public UserDashboardTicketDto(Guid id, string title, DateTimeOffset createdDate, string categpryTitle, TicketStatus status)
        {
            Id = id;
            Title = title;
            CreatedDate = createdDate.ToPersianDateTime();
            CategoryTitle = categpryTitle;
            Status = status.ToDisplay();
        }

        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string CreatedDate { get; private set; }
        public string CategoryTitle { get; private set; }
        public string Status { get; private set; }
    }
}
