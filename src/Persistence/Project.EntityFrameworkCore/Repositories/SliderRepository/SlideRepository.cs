using Microsoft.EntityFrameworkCore;
using Project.Domain;
using Project.EntityFrameworkCore;
using Project.Domain.Models.SliderEntitie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Application.Contracts.Repositories.SliderRepositories;

namespace Project.EntityFrameworkCore.Repositories.SliderRepository
{
    public class SlideRepository : Repository<Slide>, ISlideRepository
    {
        public SlideRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<Slide> GetSlideByViewOrder(int viewId)
        {
            return await DbContext.Set<Slide>().FirstOrDefaultAsync(x => x.AppearanceOrder == viewId);

        }

        public async Task<IEnumerable<Slide>> GetSlidesByStatus(bool status)
        {
            return await DbContext.Set<Slide>().Where(x => x.Status == status)
                .OrderBy(x => x.AppearanceOrder).ToListAsync();
        }
        
        public override async Task<IEnumerable<Slide>> GetAllAsync(ISpecification<Slide> spec)
        {
            return (await base.GetAllAsync()).OrderBy(x => x.AppearanceOrder).ToList();
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
