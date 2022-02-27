using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Domain.Models.CategoryEntities;

namespace Project.Domain.Contracts.Repositories.CategoryRepositories
{
    public interface IPostCategoryRepository : IRepository<PostCategory>
    {
    }
}
