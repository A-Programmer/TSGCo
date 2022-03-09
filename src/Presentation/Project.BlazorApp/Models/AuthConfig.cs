namespace Project.BlazorApp.Models;

public class AuthConfig
{
    public string IdPUrl { get; set; }
    public string AccessDeniedPath { get; set; }
    public string LoginPath { get; set; }
    public string LogoutPath { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
}