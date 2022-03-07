using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Repositories.DynamicPageRepositories;
using Project.Domain.Models.PageEntities;
using Project.EntityFrameworkCore;

namespace Project.EntityFrameworkCore.Repositories.DynamicPageRepositories
{
    public class DynamicPageRepository : Repository<DynamicPage>, IDynamicPageRepository
    {
        public DynamicPageRepository(ProjectDbContext db)
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
