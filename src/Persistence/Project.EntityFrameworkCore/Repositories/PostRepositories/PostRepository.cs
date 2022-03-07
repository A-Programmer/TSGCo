using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Repositories.PostRepositories;
using Project.Domain;
using Project.Domain.Models.PostEntities;
using Project.Domain.Models.UserEntities;
using Project.EntityFrameworkCore;

namespace Project.EntityFrameworkCore.Repositories.BlogRepositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {

        public PostRepository(ProjectDbContext db)
            : base(db)
        {
        }

        public async Task<IEnumerable<Post>> GetAllAsync(bool includeComments = false, bool includeViews = false,
            bool includeVotes = false, ISpecification<Post> specs = null)
        {
            var result = Entity.AsQueryable();
            if(specs != null)
                result = result.Where(specs.ToExpression());
            if (includeComments)
                result = result.Include(x => x.Comments);
            if (includeViews)
                result = result.Include(x => x.Views);
            //if (includeVotes)
            //    result = result.Include(x => x.Votes);

            return await result.ToListAsync();
        }

        //public override async Task<IEnumerable<Post>> GetAllAsync(ISpecification<Post> specs = null)
        //{
        //    var query = Entity.AsQueryable();
        //    if(specs != null)
        //    {
        //        query = query.Where(specs.ToExpression());
        //    }
        //    query = query;

        //    return await query.ToListAsync();
        //}

    }
}
