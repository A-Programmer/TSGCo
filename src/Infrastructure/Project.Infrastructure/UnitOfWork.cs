using System;
using System.Threading.Tasks;
using Project.Domain;
using Project.Domain.Contracts.Repositories.AnnouncementRepositories;
using Project.Domain.Contracts.Repositories.CategoryRepositories;
using Project.Domain.Contracts.Repositories.ContactUsRepositories;
using Project.Domain.Contracts.Repositories.DynamicPageRepositories;
using Project.Domain.Contracts.Repositories.MenuRepositories;
using Project.Domain.Contracts.Repositories.PostRepositories;
using Project.Domain.Contracts.Repositories.RoleRepositories;
using Project.Domain.Contracts.Repositories.SliderRepositories;
using Project.Domain.Contracts.Repositories.StaticContentRepositories;
using Project.Domain.Contracts.Repositories.UserRepositories;
using Project.Infrastructure.Repositories.AnnouncementRepositories;
using Project.Infrastructure.Repositories.BlogRepositories;
using Project.Infrastructure.Repositories.ContactUsRepositories;
using Project.Infrastructure.Repositories.DynamicPageRepositories;
using Project.Infrastructure.Repositories.MenuRepositories;
using Project.Infrastructure.Repositories.RoleRepositories;
using Project.Infrastructure.Repositories.SliderRepository;
using Project.Infrastructure.Repositories.StaticContentRepositories;
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
