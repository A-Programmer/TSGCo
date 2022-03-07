using System;
namespace Project.Application.Dtos.AnnouncementDtos
{
    public class AddAnnouncementResponse
    {
        public AddAnnouncementResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
