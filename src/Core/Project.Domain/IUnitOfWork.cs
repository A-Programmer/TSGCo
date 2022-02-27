using System;
using System.Threading.Tasks;
using Project.Domain.Contracts.Repositories.AnnouncementRepositories;
using Project.Domain.Contracts.Repositories.PostRepositories;
using Project.Domain.Contracts.Repositories.ContactUsRepositories;
using Project.Domain.Contracts.Repositories.DynamicPageRepositories;
using Project.Domain.Contracts.Repositories.UserRepositories;
using Project.Domain.Contracts.Repositories.MenuRepositories;
using Project.Domain.Contracts.Repositories.SliderRepositories;
using Project.Domain.Contracts.Repositories.CategoryRepositories;
using Project.Domain.Contracts.Repositories.RoleRepositories;
using Project.Domain.Contracts.Repositories.StaticContentRepositories;

namespace Project.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IPostCategoryRepository Categories { get; }
        IPostRepository Posts { get; }
        IUserRepository Users { get; }
        IRoleRepository Roles { get; }
        IContactUsRepository ContactUs { get; }
        IAnnouncementRepository Announcements { get; }
        IMenuRepository Menus { get; }
        IDynamicPageRepository DynamicPages { get; }
        ISlideRepository Slide { get; }
        IStaticContentRepository StaticContents { get; }

        Task<int> CommitAsync();
    }
}
