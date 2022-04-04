namespace Project.CMS.Models;

public class AuthConfig
{
    public string IdPUrl { get; set; }
    public string AccessDeniedPath { get; set; }
    public string LoginPath { get; set; }
    public string LogoutPath { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }

    public string DefaultScheme { get; set; }
    public string DefaultChallengeScheme { get; set; }
    public string SignInScheme { get; set; }
    public string ResponseType { get; set; }
    public bool SaveTokens { get; set; }
    public bool GetClaimsFromUserInfoEndpoint { get; set; }

}