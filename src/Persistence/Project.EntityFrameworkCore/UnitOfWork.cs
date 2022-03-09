using System;
using System.Threading.Tasks;
using Project.Application;
using Project.Application.Contracts.Repositories.CategoryRepositories;
using Project.Application.Contracts.Repositories.PostRepositories;
using Project.Application.Contracts.Repositories.RoleRepositories;
using Project.Application.Contracts.Repositories.UserRepositories;
using Project.EntityFrameworkCore.Repositories.BlogRepositories;
using Project.EntityFrameworkCore.Repositories.RoleRepositories;
using Project.EntityFrameworkCore.Repositories.UserRepositories;

namespace Project.EntityFrameworkCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectDbContext _db;
        public UnitOfWork(ProjectDbContext db) => _db = db;

        private PostCategoryRepository _categories;
        public IPostCategoryRepository Categories => _categories ??= new PostCategoryRepository(_db);

        private PostRepository _posts;
        public IPostRepository Posts => _posts ??= new PostRepository(_db);

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
