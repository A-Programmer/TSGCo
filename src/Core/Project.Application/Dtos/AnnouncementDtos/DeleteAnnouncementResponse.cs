using System;
using MediatR;

namespace Project.Application.Dtos.AnnouncementDtos
{
    public class DeleteAnnouncementResponse
    {
        public DeleteAnnouncementResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
