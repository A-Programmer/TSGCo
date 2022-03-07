using System;
using System.Threading.Tasks;
using Project.Application.Contracts.Repositories.AnnouncementRepositories;
using Project.Application.Contracts.Repositories.PostRepositories;
using Project.Application.Contracts.Repositories.ContactUsRepositories;
using Project.Application.Contracts.Repositories.DynamicPageRepositories;
using Project.Application.Contracts.Repositories.UserRepositories;
using Project.Application.Contracts.Repositories.MenuRepositories;
using Project.Application.Contracts.Repositories.SliderRepositories;
using Project.Application.Contracts.Repositories.CategoryRepositories;
using Project.Application.Contracts.Repositories.RoleRepositories;
using Project.Application.Contracts.Repositories.StaticContentRepositories;

namespace Project.Application
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
