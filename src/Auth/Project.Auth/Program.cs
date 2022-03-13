using Microsoft.EntityFrameworkCore;
using Project.Auth;
using Project.Auth.Contracts;
using Project.Auth.Data;
using Project.Auth.Services;
using Project.Auth.Utilities;

const string TwoFactorAuthenticationScheme = "idsrv.2FA";

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

builder.Services.AddScoped<IUnitOfWork, ApplicationDbContext>();
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IUserClaimsService, UserClaimsService>();
builder.Services.AddScoped<IConfigSeedDataService, ConfigSeedDataService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        Configuration.GetConnectionString("AuthServiceConnectionString"),
        serverDbContextOptionsBuilder =>
        {
            var minutes = (int)TimeSpan.FromMinutes(3).TotalSeconds;
            serverDbContextOptionsBuilder.CommandTimeout(minutes);
            serverDbContextOptionsBuilder.EnableRetryOnFailure();
        });
});

builder.Services.AddTransient<DataSeeder>();

builder.Services.AddControllersWithViews();

var identityServer = builder.Services.AddIdentityServer();

identityServer
    .AddDeveloperSigningCredential()
    .AddCustomUserStore()
    .AddConfigurationStore()
    .AddOperationalStore();
    // .AddInMemoryIdentityResources(Config.GetIdentityResources())
    // .AddInMemoryApiResources(Config.GetApiResources())
    // .AddInMemoryApiScopes(Config.GetApiScopes())
    // .AddInMemoryClients(Config.GetClients());


builder.Services.AddAuthentication();
    // .AddCookie(authenticationScheme: TwoFactorAuthenticationScheme)
    // .AddGoogle(authenticationScheme: "Google", configureOptions: options =>
    // {
    //     options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
    //     options.ClientId = Configuration["Authentication:Google:ClientId"];
    //     options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
    // });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    MigrateDbAndSeed(app);
}

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

void MigrateDbAndSeed(IHost app)
{
    var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
    using (var scope = scopeFactory.CreateScope())
    {
        using (var context = scope.ServiceProvider.GetService<ApplicationDbContext>())
        {
            context.Database.Migrate();
        }
        var service = scope.ServiceProvider.GetService<DataSeeder>();
        service.Seed();
    }
}
