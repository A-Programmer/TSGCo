using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Project.AdminUI.Models;

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
AuthConfig authConfig = Configuration.GetSection("AuthConfig").Get<AuthConfig>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
    })
    .AddCookie("Cookies", options =>
    {
        options.AccessDeniedPath = authConfig.AccessDeniedPath;
        options.LoginPath = authConfig.LoginPath;
        options.LogoutPath = authConfig.LogoutPath;
    })
    .AddOpenIdConnect("oidc", options =>
    {
        options.SignInScheme = "Cookies";
        options.Authority = authConfig.IdPUrl;
        options.ClientId = authConfig.ClientId;
        options.ResponseType = "code id_token";
        options.Scope.Add("openid");
        options.Scope.Add("address");
        options.Scope.Add("profile");
        options.Scope.Add("country");
        options.Scope.Add("roles");
        options.Scope.Add("offline_access");
        options.SaveTokens = true;
        options.ClientSecret = authConfig.ClientSecret;
        options.GetClaimsFromUserInfoEndpoint = true;

        options.ClaimActions.Remove("amr");
        options.ClaimActions.DeleteClaim("sid");
        options.ClaimActions.DeleteClaim("idp");

        options.ClaimActions.MapUniqueJsonKey("address", "address");
        options.ClaimActions.MapUniqueJsonKey("country", "country");
        options.ClaimActions.MapUniqueJsonKey("role", "role");
        options.ClaimActions.MapUniqueJsonKey("subscription", "subscription");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = JwtClaimTypes.GivenName,
            RoleClaimType = JwtClaimTypes.Role,
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
