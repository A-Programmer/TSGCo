// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


#pragma warning disable 1591

using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class Client
    {
        public int Id { get; set; }
        public bool Enabled { get; set; } = true;
        public string ClientId { get; set; }
        public string ProtocolType { get; set; } = IdentityServerConstants.ProtocolTypes.OpenIdConnect;
        public List<ClientSecret> ClientSecrets { get; set; }
        public bool RequireClientSecret { get; set; } = true;
        public string ClientName { get; set; }
        public string Description { get; set; }
        public string ClientUri { get; set; }
        public string LogoUri { get; set; }
        public bool RequireConsent { get; set; } = true;
        public bool AllowRememberConsent { get; set; } = true;
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; } = true;
        public List<ClientGrantType> AllowedGrantTypes { get; set; }
        public bool RequirePkce { get; set; }
        public bool AllowPlainTextPkce { get; set; }
        public bool AllowAccessTokensViaBrowser { get; set; }
        public List<ClientRedirectUri> RedirectUris { get; set; }
        public List<ClientPostLogoutRedirectUri> PostLogoutRedirectUris { get; set; }
        public string FrontChannelLogoutUri { get; set; }
        public bool FrontChannelLogoutSessionRequired { get; set; } = true;
        public string BackChannelLogoutUri { get; set; }
        public bool BackChannelLogoutSessionRequired { get; set; } = true;
        public bool AllowOfflineAccess { get; set; }
        public List<ClientScope> AllowedScopes { get; set; }
        public int IdentityTokenLifetime { get; set; } = 300;
        public int AccessTokenLifetime { get; set; } = 3600;
        public int AuthorizationCodeLifetime { get; set; } = 300;
        public int? ConsentLifetime { get; set; } = null;
        public int AbsoluteRefreshTokenLifetime { get; set; } = 2592000;
        public int SlidingRefreshTokenLifetime { get; set; } = 1296000;
        public int RefreshTokenUsage { get; set; } = (int)TokenUsage.OneTimeOnly;
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
        public int RefreshTokenExpiration { get; set; } = (int)TokenExpiration.Absolute;
        public int AccessTokenType { get; set; } = (int)0; // AccessTokenType.Jwt;
        public bool EnableLocalLogin { get; set; } = true;
        public List<ClientIdPRestriction> IdentityProviderRestrictions { get; set; }
        public bool IncludeJwtId { get; set; }
        public List<ClientClaim> Claims { get; set; }
        public bool AlwaysSendClientClaims { get; set; }
        public string ClientClaimsPrefix { get; set; } = "client_";
        public string PairWiseSubjectSalt { get; set; }
        public List<ClientCorsOrigin> AllowedCorsOrigins { get; set; }
        public List<ClientProperty> Properties { get; set; }
    }

    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> client)
        {
            client.HasKey(x => x.Id);

            client.Property(x => x.ClientId).HasMaxLength(200).IsRequired();
            client.Property(x => x.ProtocolType).HasMaxLength(200).IsRequired();
            client.Property(x => x.ClientName).HasMaxLength(200);
            client.Property(x => x.ClientUri).HasMaxLength(2000);
            client.Property(x => x.LogoUri).HasMaxLength(2000);
            client.Property(x => x.Description).HasMaxLength(1000);
            client.Property(x => x.FrontChannelLogoutUri).HasMaxLength(2000);
            client.Property(x => x.BackChannelLogoutUri).HasMaxLength(2000);
            client.Property(x => x.ClientClaimsPrefix).HasMaxLength(200);
            client.Property(x => x.PairWiseSubjectSalt).HasMaxLength(200);

            client.HasIndex(x => x.ClientId).IsUnique();

            client.HasMany(x => x.AllowedGrantTypes).WithOne(x => x.Client).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            client.HasMany(x => x.RedirectUris).WithOne(x => x.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
            client.HasMany(x => x.PostLogoutRedirectUris).WithOne(x => x.Client).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            client.HasMany(x => x.AllowedScopes).WithOne(x => x.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
            client.HasMany(x => x.ClientSecrets).WithOne(x => x.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
            client.HasMany(x => x.Claims).WithOne(x => x.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);
            client.HasMany(x => x.IdentityProviderRestrictions).WithOne(x => x.Client).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            client.HasMany(x => x.AllowedCorsOrigins).WithOne(x => x.Client).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            client.HasMany(x => x.Properties).WithOne(x => x.Client).IsRequired().OnDelete(DeleteBehavior.Cascade);

            // client.HasData(
            //     new Client
            //     {
            //         Enabled = true,
            //         ClientId = "cmsclient",
            //         ClientSecrets = 
            //         {
            //             new ClientSecret
            //             {
            //                 Id = 1,
            //                 Description = "Description",
            //                 Type = "Type of secret",
            //                 Value = "CMSClientSecretKeyGoesHere",
            //                 Expiration = DateTime.Now.AddYears(1)
            //             }
            //         },
            //         IdentityTokenLifetime = 300,
            //         AuthorizationCodeLifetime = 300,
            //         AccessTokenLifetime = 300,
            //         AllowOfflineAccess = true,
            //         UpdateAccessTokenClaimsOnRefresh = true,
            //         AbsoluteRefreshTokenLifetime = 2592000,
            //         AllowedGrantTypes = {
            //             new ClientGrantType
            //             {
            //                 Id = 1,
            //                 GrantType = "code id_token"
            //             }
            //         },
            //         RequirePkce = false,
            //         RequireConsent = true,                     
            //         RedirectUris = 
            //         {
            //             new ClientRedirectUri
            //             {
            //                 Id = 1,
            //                 RedirectUri = "https://localhost:8001/signin-oidc"
            //             }
            //         },
            //         PostLogoutRedirectUris = 
            //         {
            //             new ClientPostLogoutRedirectUri
            //             {
            //                 Id = 1,
            //                 PostLogoutRedirectUri = "https://localhost:8001/signout-callback-oidc"
            //             }
            //         },
            //         AllowedScopes =
            //         {
            //             new ClientScope
            //             {
            //                 Id = 1,
            //                 Scope = "roles"
            //             },
            //             new ClientScope
            //             {
            //                 Id = 2,
            //                 Scope = "address"
            //             },
            //             new ClientScope
            //             {
            //                 Id = 3,
            //                 Scope = "openid"
            //             },
            //             new ClientScope
            //             {
            //                 Id = 3,
            //                 Scope = "profile"
            //             }
            //         },
            //         ProtocolType = IdentityServerConstants.ProtocolTypes.OpenIdConnect,
            //         Claims = 
            //         {
            //             new ClientClaim
            //             {
            //                 Id = 1,
            //                 Type = "roles",
            //                 Value = "admin"
            //             }
            //         },
            //         IdentityProviderRestrictions = 
            //         {
            //             new ClientIdPRestriction
            //             {
            //                 Id = 1,
            //                 Provider = "PROVIDER"
            //             }
            //         },
            //         AllowedCorsOrigins = 
            //         {
            //             new ClientCorsOrigin
            //             {
            //                 Id = 1,
            //                 Origin = "all"
            //             }
            //         },
            //         Properties = 
            //         {
            //             new ClientProperty
            //             {
            //                 Id = 1,
            //                 Key = "Client Property item",
            //                 Value = "Value of client property"
            //             }
            //         },
            //         BackChannelLogoutUri = "",
            //         ClientUri = "https://localhost:7001"
                    
            //     }
            // );
        }
    }
}