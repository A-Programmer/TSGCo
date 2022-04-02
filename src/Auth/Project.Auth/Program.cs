using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Project.Auth.Data;
using Project.Auth.Domain;
using Project.Auth.Services;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);
IConfiguration Configuration;

if(builder.Environment.IsProduction())
{
    Configuration = Configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.Production.json")
                            .Build();
}
else
{
    Configuration = Configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.Development.json")
                            .Build();
}

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("AuthServiceConnectionString"))
);
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>, 
        UserClaimsPrincipalFactory<User>>();

builder.Services.AddTransient<IProfileService, ProfileService>();

IdentityModelEventSource.ShowPII = true;

builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddAspNetIdentity<User>()
    .AddProfileService<ProfileService>()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(Configuration.GetConnectionString("AuthServiceConnectionString"), sql => sql.MigrationsAssembly("Project.Auth"));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b => b.UseSqlServer(Configuration.GetConnectionString("AuthServiceConnectionString"), sql => sql.MigrationsAssembly("Project.Auth"));
    });
    
builder.Services.AddLocalApiAuthentication();

builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IClientServices, ClientServices>();
builder.Services.AddScoped<IUnitOfWork, ApplicationDbContext>();



var app = builder.Build();

MigrateDb(app);

DatabaseInitializer.PopulateIdentityServer(app);

app.UseIdentityServer();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "AdminArea",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();


void MigrateDb(IHost app)
{
    var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
    using (var scope = scopeFactory.CreateScope())
    {
        using (var context = scope.ServiceProvider.GetService<ApplicationDbContext>())
        {
            context.Database.Migrate();
        }
    }
}