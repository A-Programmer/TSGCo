using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Common;
using Project.Common.Exceptions;
using Project.Domain.Contracts.Repositories.AnnouncementRepositories;
using Project.Domain.Models.AnnouncementEntities;

namespace Project.Infrastructure.Repositories.AnnouncementRepositories
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
