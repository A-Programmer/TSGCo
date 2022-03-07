using System;
namespace Project.Application.Dtos.AnnouncementDtos
{
    public class EditAnnouncementResponse
    {
        public EditAnnouncementResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
