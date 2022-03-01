using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Contracts.Repositories.StaticContentRepositories;
using Project.Domain.Models.StaticContentEntities;

namespace Project.Infrastructure.Repositories.StaticContentRepositories
{
    public class StaticContentRepository : Repository<StaticContent>, IStaticContentRepository
    {
        public StaticContentRepository(ProjectDbContext db)
            : base(db)
        {
        }

        public async Task<StaticContent> GetByTitle(string title)
        {
            return await Entity.FirstOrDefaultAsync(x => x.Title.ToLower() == title.ToLower());
        }
    }
}
