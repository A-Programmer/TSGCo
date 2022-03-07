using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.EntityFrameworkCore;
using Project.Domain.Models.MenuEntities;
using Project.Application.Contracts.Repositories.MenuRepositories;

namespace Project.EntityFrameworkCore.Repositories.MenuRepositories
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
