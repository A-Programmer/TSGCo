using System;
using System.Threading.Tasks;
using Project.Application;
using Project.Application.Contracts.Repositories.AnnouncementRepositories;
using Project.Application.Contracts.Repositories.CategoryRepositories;
using Project.Application.Contracts.Repositories.ContactUsRepositories;
using Project.Application.Contracts.Repositories.DynamicPageRepositories;
using Project.Application.Contracts.Repositories.MenuRepositories;
using Project.Application.Contracts.Repositories.PostRepositories;
using Project.Application.Contracts.Repositories.RoleRepositories;
using Project.Application.Contracts.Repositories.SliderRepositories;
using Project.Application.Contracts.Repositories.StaticContentRepositories;
using Project.Application.Contracts.Repositories.UserRepositories;
using Project.EntityFrameworkCore.Repositories.AnnouncementRepositories;
using Project.EntityFrameworkCore.Repositories.BlogRepositories;
using Project.EntityFrameworkCore.Repositories.ContactUsRepositories;
using Project.EntityFrameworkCore.Repositories.DynamicPageRepositories;
using Project.EntityFrameworkCore.Repositories.MenuRepositories;
using Project.EntityFrameworkCore.Repositories.RoleRepositories;
using Project.EntityFrameworkCore.Repositories.SliderRepository;
using Project.EntityFrameworkCore.Repositories.StaticContentRepositories;
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

        private ContactUsRepository _contactUs;
        public IContactUsRepository ContactUs => _contactUs ??= new ContactUsRepository(_db);

        private AnnouncementRepository _announcements;
        public IAnnouncementRepository Announcements => _announcements ??= new AnnouncementRepository(_db);

        private MenuRepository _menus;
        public IMenuRepository Menus => _menus ??= new MenuRepository(_db);

        private DynamicPageRepository _dynamicPages;
        public IDynamicPageRepository DynamicPages => _dynamicPages ??= new DynamicPageRepository(_db);

        private SlideRepository _slide;
        public ISlideRepository Slide => _slide ??= new SlideRepository(_db);

        private StaticContentRepository _staticContents;
        public IStaticContentRepository StaticContents => _staticContents ??= new StaticContentRepository(_db);


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
