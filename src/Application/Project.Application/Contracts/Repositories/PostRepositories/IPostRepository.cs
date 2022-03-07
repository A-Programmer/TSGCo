using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Domain;
using Project.Domain.Models.PostEntities;

namespace Project.Application.Contracts.Repositories.PostRepositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetAllAsync(bool includeComments = false, bool includeViews = false,
            bool includeVotes = false, ISpecification<Post> specs = null);
    }
}
