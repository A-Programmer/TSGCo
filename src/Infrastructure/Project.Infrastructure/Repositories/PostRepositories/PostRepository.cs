using Project.Domain.Contracts.Repositories.PostRepositories;
using Project.Domain.Models.PostEntities;

namespace Project.Infrastructure.Repositories.BlogRepositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {

        public PostRepository(ProjectDbContext db)
            : base(db)
        {
        }

    }
}
