using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Project.Auth.Domain;

namespace Project.Auth.Data
{
    public static class DatabaseInitializer
    {
        public static async void PopulateIdentityServer(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            var db = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.Migrate();
            if(!context.Clients.Any())
            {
                foreach (var client in Config.Clients)
                {
                    var item = context.Clients.SingleOrDefault(c => c.ClientName == client.ClientId);

                    if (item == null)
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                }
            }

            if(!context.ApiResources.Any())
            {
                foreach (var resource in Config.ApiResources)
                {
                    var item = context.ApiResources.SingleOrDefault(c => c.Name == resource.Name);

                    if (item == null)
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                }
            }

            if(!context.ApiScopes.Any())
            {
                foreach (var scope in Config.ApiScopes)
                {
                    var item = context.ApiScopes.SingleOrDefault(c => c.Name == scope.Name);

                    if (item == null)
                    {
                        context.ApiScopes.Add(scope.ToEntity());
                    }
                }
            }


            context.SaveChanges();

            if(db.Users.Include(x => x.Profile).Any())
            {
                var admin = await db.Users.FirstOrDefaultAsync(x => x.UserName == "admin");
                if(admin != null && admin.Profile != null)
                {
                    var adminProfile = new UserProfile("کامران", "سادین");
                    admin.SetProfile(adminProfile);

                    await db.SaveChangesAsync();
                }
            }
        }
    }
}