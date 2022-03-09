using System;
using Microsoft.Extensions.DependencyInjection;
using Project.Auth.Services.CustomProfiles;

namespace Project.Auth.Utilities
{
    public static class IdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddCustomUserStore(this IIdentityServerBuilder builder)
        {
            // builder.Services.AddScoped<IUsersService, UsersService>();
            builder.AddProfileService<CustomUserProfileService>();
            return builder;
        }
    }
}
