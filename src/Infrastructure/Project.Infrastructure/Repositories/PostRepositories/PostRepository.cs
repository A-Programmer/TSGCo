using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Domain;
using Project.Domain.Contracts.Repositories.PostRepositories;
using Project.Domain.Models.PostEntities;
using Project.Domain.Models.UserEntities;

namespace Project.Infrastructure.Repositories.BlogRepositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {

        public PostRepository(ProjectDbContext db)
            : base(db)
        {
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
