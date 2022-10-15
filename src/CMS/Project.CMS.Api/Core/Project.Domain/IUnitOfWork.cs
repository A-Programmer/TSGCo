using System;
using System.Threading.Tasks;
using Project.Domain.Contracts.Repositories.UserRepositories;
using Project.Domain.Contracts.Repositories.RoleRepositories;

namespace Project.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        Task<int> CommitAsync();
    }
}
