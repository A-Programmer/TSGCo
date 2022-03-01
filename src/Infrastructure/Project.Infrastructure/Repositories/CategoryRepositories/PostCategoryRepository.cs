using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Common;
using Project.Common.Exceptions;
using Project.Domain.Contracts.Repositories.CategoryRepositories;
using Project.Domain.Models.CategoryEntities;
using Project.Domain.Models.PostEntities;

namespace Project.Infrastructure.Repositories.BlogRepositories
{
    public class PostCategoryRepository : Repository<PostCategory> , IPostCategoryRepository
    {
        public PostCategoryRepository(ProjectDbContext db)
            : base(db)
        {
        }

        public async Task<List<PostCategory>> GetCategoriesByIds(Guid[] ids)
        {
            return await DbContext.Set<PostCategory>().Where(x => ids.Contains(x.Id)).ToListAsync();
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
