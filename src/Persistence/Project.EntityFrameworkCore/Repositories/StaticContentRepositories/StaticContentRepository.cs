using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.EntityFrameworkCore;
using Project.Domain.Models.StaticContentEntities;
using Project.Application.Contracts.Repositories.StaticContentRepositories;

namespace Project.EntityFrameworkCore.Repositories.StaticContentRepositories
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
