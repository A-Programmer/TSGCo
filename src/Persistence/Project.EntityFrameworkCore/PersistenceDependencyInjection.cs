using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Contracts.Repositories.PostRepositories;
using Project.EntityFrameworkCore.Repositories.BlogRepositories;

namespace Project.Application
{
    public static class PersistenceDependencyInjection
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPostRepository, PostRepository>();
        }
    }
}