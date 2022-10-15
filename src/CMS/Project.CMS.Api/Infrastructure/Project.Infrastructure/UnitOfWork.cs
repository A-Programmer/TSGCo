using System;
using System.Threading.Tasks;
using Project.Domain;
using Project.Domain.Contracts.Repositories.RoleRepositories;
using Project.Domain.Contracts.Repositories.UserRepositories;
using Project.Infrastructure.Repositories.RoleRepositories;
using Project.Infrastructure.Repositories.UserRepositories;

namespace Project.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectDbContext _db;
        public UnitOfWork(ProjectDbContext db)
        {
            _db = db;
        }

        private UserRepository _users;
        public IUserRepository Users => _users ??= new UserRepository(_db);

        private RoleRepository _roles;
        public IRoleRepository Roles => _roles ??= new RoleRepository(_db);

        public async Task<int> CommitAsync()
        {
            return await _db.SaveChangesAsync();
        }
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
