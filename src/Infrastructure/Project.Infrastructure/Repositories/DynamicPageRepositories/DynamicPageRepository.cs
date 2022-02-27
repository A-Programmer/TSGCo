using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Common;
using Project.Common.Exceptions;
using Project.Domain.Contracts.Repositories.DynamicPageRepositories;
using Project.Domain.Models.PageEntities;

namespace Project.Infrastructure.Repositories.DynamicPageRepositories
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
