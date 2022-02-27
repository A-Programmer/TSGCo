using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Common;
using Project.Common.Exceptions;
using Project.Domain.Contracts.Repositories.MenuRepositories;
using Project.Domain.Models.MenuEntities;

namespace Project.Infrastructure.Repositories.MenuRepositories
{
    public class MenuRepository : Repository<MenuItem>, IMenuRepository
    {
        public MenuRepository(ProjectDbContext db)
            : base(db)
        {
        }

        

        public async Task<IEnumerable<MenuItem>> GetChildren(Guid parentId)
        {
            return await DbContext.Set<MenuItem>().Where(x => x.ParentId == parentId).ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> GetParents()
        {
            return await DbContext.Set<MenuItem>().Where(x => x.ParentId == null).ToListAsync();
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
