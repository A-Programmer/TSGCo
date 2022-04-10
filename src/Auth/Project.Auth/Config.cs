// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Project.Auth
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("country", "Country", new List<string> { "country" }),
                new IdentityResource("website", "Website", new List<string> { "website" })
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            { 
                new ApiScope("cmsapi", "CMS Api"),
                new ApiScope("cmsapi2", "CMS Api2")
            };

        public static IEnumerable<Client> Clients =>
            new Client[] 
            {
                new Client
                {
                    IdentityTokenLifetime = 300,
                    AuthorizationCodeLifetime = 300,
                    AccessTokenLifetime = 300,

                    AllowOfflineAccess = true,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    AbsoluteRefreshTokenLifetime = 2592000,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    ClientId = "cmsclient",
                    ClientName = "CMS Client",
                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RedirectUris = 
                    {
                        "https://localhost:7001/signin-oidc"
                    },
                    PostLogoutRedirectUris = 
                    {
                        "https://localhost:7001/signout-callback-oidc"
                    },
                    ClientSecrets = { new Secret("CMSClientSecretKeyGoesHere".Sha256()) },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "country",
                        "cmsapi",
                        "cmsapi2",
                        "website"
                    }
                }
            };
    }
}