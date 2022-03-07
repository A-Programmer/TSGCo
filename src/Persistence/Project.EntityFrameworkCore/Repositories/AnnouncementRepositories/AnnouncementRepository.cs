
using Project.Application.Contracts.Repositories.AnnouncementRepositories;
using Project.Domain.Models.AnnouncementEntities;
using Project.EntityFrameworkCore;

namespace Project.EntityFrameworkCore.Repositories.AnnouncementRepositories
{
    public class AnnouncementRepository : Repository<Announcement>, IAnnouncementRepository
    {
        public AnnouncementRepository(ProjectDbContext db)
            : base(db)
        {
        }


        private ProjectDbContext DbContext
        {
            get
            {
                return Context as ProjectDbContext;
            }
        }
    }
}
