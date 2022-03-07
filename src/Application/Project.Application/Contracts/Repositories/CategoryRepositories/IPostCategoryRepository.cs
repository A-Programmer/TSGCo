using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Domain.Models.CategoryEntities;

namespace Project.Application.Contracts.Repositories.CategoryRepositories
{
    public interface IPostCategoryRepository : IRepository<PostCategory>
    {
        Task<List<PostCategory>> GetCategoriesByIds(Guid[] ids);
    }
}
