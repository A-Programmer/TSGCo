using System;
using MediatR;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Project.Application
{
    public static class ApplicationDependencyInjection
    {
        public static void RegisterMediatR(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}