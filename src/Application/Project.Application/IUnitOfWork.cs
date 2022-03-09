using System;
using System.Threading.Tasks;
using Project.Application.Contracts.Repositories.PostRepositories;
using Project.Application.Contracts.Repositories.UserRepositories;
using Project.Application.Contracts.Repositories.CategoryRepositories;
using Project.Application.Contracts.Repositories.RoleRepositories;

namespace Project.Application
{
    public interface IUnitOfWork : IDisposable
    {
        IPostCategoryRepository Categories { get; }
        IPostRepository Posts { get; }
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }

        Task<int> CommitAsync();
    }
}
