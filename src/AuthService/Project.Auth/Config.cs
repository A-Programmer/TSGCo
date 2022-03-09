using System;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace Project.Auth
{
    public static class Config
    {

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "d860efca-22d9-47fd-8249-791ba61b07c7",
                    Username = "admin",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Kamran"),
                        new Claim("family_name", "Sadin"),
                        new Claim("address", "Iran, Golestan, Gonbade Kavous"),
                        new Claim("country", "iran"),
                        new Claim("role","admin"),
                        new Claim("subscription","Golden")
                    }
                },
                new TestUser
                {
                    SubjectId = "b7539694-97e7-4dfe-84da-b4256e1ff5c7",
                    Username = "user",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "John"),
                        new Claim("family_name", "Doe"),
                        new Claim("address", "Iran, Tehran"),
                        new Claim("country", "USA"),
                        new Claim("role","user"),
                        new Claim("subscription", "Silver")
                    }
                }
            };
        }


        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Address(),
                new IdentityResources.Profile(),
                new IdentityResource(
                    name: "country",
                    displayName: "Your Country",
                    userClaims: new List<string>() { "country" }),
                new IdentityResource(
                    name: "roles",
                    displayName: "Your Roles",
                    userClaims: new List<string>() { "role" }),
                new IdentityResource(
                    name: "subscription",
                    displayName: "Subscription Level",
                    userClaims: new List<string> { "subscription" }
                    )
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                //Admin Panel : .NET Core MVC App
                new Client
                {
                    IdentityTokenLifetime = 300,
                    AuthorizationCodeLifetime = 300,
                    AccessTokenLifetime = 300,

                    AllowOfflineAccess = true,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    AbsoluteRefreshTokenLifetime = 2592000,
                    RefreshTokenExpiration = TokenExpiration.Sliding,

                    AccessTokenType = AccessTokenType.Reference,
                    
                    ClientName = "Admin Panel",
                    ClientId = "adminpanel",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RequirePkce = false,
                    RequireConsent = true,                     
                    RedirectUris = 
                    {
                        "https://localhost:8001/signin-oidc"
                    },
                    PostLogoutRedirectUris = 
                    {
                        "https://localhost:8001/signout-callback-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles",
                        "cms.posts.list",
                        "cms.posts.add",
                        "cms.posts.update",
                        "cms.posts.delete",
                        "cms.posts.comments.list",
                        "cms.posts.comments.delete",
                        "cms.posts.keywords.list",
                        "cms.posts.keywords.add",
                        "cms.posts.keywords.update",
                        "cms.posts.keywords.delete"
                    },
                    ClientSecrets =
                    {
                        new Secret("AdminPanelClientPassword".Sha256())
                    }

                },
                //CMS Client : Blazor Server App
                new Client
                {
                    IdentityTokenLifetime = 300,
                    AuthorizationCodeLifetime = 300,
                    AccessTokenLifetime = 300,

                    AllowOfflineAccess = true,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    AbsoluteRefreshTokenLifetime = 2592000,
                    RefreshTokenExpiration = TokenExpiration.Sliding,

                    AccessTokenType = AccessTokenType.Reference,
                    
                    ClientName = "CMS Client",
                    ClientId = "cmsclient",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RequirePkce = false,
                    RequireConsent = true,                     
                    RedirectUris = 
                    {
                        "https://localhost:7001/signin-oidc"
                    },
                    PostLogoutRedirectUris = 
                    {
                        "https://localhost:7001/signout-callback-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Address,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles"
                    },
                    ClientSecrets =
                    {
                        new Secret("CMSClientSecretKeyGoesHere".Sha256())
                    }

                }
             };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("contacts", "Contacts API Resourse", userClaims: new List<string> { "role" })
                {
                    Scopes =
                    {
                        "contactsapi.getall",
                        "contactsapi.getbyid",
                        "contactsapi.add",
                        "contactsapi.update",
                        "contactsapi.delete"
                    },
                    ApiSecrets = { new Secret("apisecret".Sha256()) }
                }
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope(
                        name:"contactsapi.getall",
                        displayName: "Get All Contacts"),
                new ApiScope(
                        name: "contactsapi.getbyid",
                        displayName: "Get Contact by ID"),
                new ApiScope(
                        name: "contactsapi.add",
                        displayName: "Add Contact"),
                new ApiScope(
                        name: "contactsapi.update",
                        displayName: "Update Contact"),
                new ApiScope(
                        name: "contactsapi.delete",
                        displayName: "Delete Contact")
            };
        }

    }
}
