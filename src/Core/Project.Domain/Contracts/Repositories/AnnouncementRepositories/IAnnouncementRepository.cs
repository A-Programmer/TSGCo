using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Domain.Models.AnnouncementEntities;

namespace Project.Domain.Contracts.Repositories.AnnouncementRepositories
{
    public interface IAnnouncementRepository : IRepository<Announcement>
    {
    }
}
