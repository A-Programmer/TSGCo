using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Project.CMS.Models;
using Project.CMS.Services;

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



builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

builder.Services.AddScoped<ITokenServices, TokenServices>();

builder.Services.AddHttpContextAccessor();

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = authConfig.DefaultScheme;
    options.DefaultChallengeScheme = authConfig.DefaultChallengeScheme;
})
.AddCookie("Cookies", options =>
{
    options.AccessDeniedPath = authConfig.AccessDeniedPath;
    options.LoginPath = authConfig.LoginPath;
    options.LogoutPath = authConfig.LogoutPath;
})
.AddOpenIdConnect("oidc", options =>
{
    options.SignInScheme = authConfig.SignInScheme;
    options.ResponseType = authConfig.ResponseType;

    options.Authority = authConfig.IdPUrl;
    options.ClientId = authConfig.ClientId;
    options.ClientSecret = authConfig.ClientSecret;

    //Scopes
    options.Scope.Add("profile");
    options.Scope.Add("openid");
    options.Scope.Add("website");
    options.Scope.Add("country");
    options.Scope.Add("cmsapi");
    options.Scope.Add("cmsapi2");
    options.Scope.Add("offline_access");

    options.ClaimActions.MapUniqueJsonKey("country", "country");
    options.ClaimActions.MapUniqueJsonKey("website", "website");

    options.SaveTokens = authConfig.SaveTokens;
    options.GetClaimsFromUserInfoEndpoint = authConfig.GetClaimsFromUserInfoEndpoint;

});

var app = builder.Build();
app.UseDeveloperExceptionPage();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoint =>
{
    endpoint.MapDefaultControllerRoute();
});

app.Run();
