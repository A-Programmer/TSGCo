// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


#pragma warning disable 1591

using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using KSFramework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class Client : BaseEntity<Guid>
    {
        public Client()
        {
        }
        public Client(string clientId, string clientName, string description, string clientUri, string logoUri, string backChannelLogoutUri, string frontChannelLogoutUri,
            bool requirePkce, bool allowPlainTextPkce, bool allowAccessTokensViaBrowser, bool allowOfflineAccess, bool updateAccessTokenClaimsOnRefresh,
            bool includeJwtId, bool alwaysSendClientClaims,

            bool isEnabled = true, bool requireClientSecret = true, bool requireConsent = true, bool allowRememberConsent = true, bool alwaysIncludeUserClaimsInIdToken = true,
            bool frontChannelLogoutSessionRequired = true, bool backChannelLogoutSessionRequired = true, bool enableLocalLogin = true,
            
            int identityTokenLifetime = 300, int accessTokenLifetime = 3600, int authorizationCodeLifetims = 300, int absoluteRefreshTokenLifetime = 2592000,
            int slidingRefreshTokenLifetime = 1296000, int refreshTokenUsage = (int)TokenUsage.OneTimeOnly, int refreshTokenExpiration = (int)TokenExpiration.Absolute,
            int accessTokenType = (int)0,
            string clientClaimsPrefix = "client_", string protocolType = "oidc", string pairWiseSubjectSalt = "")
        {
            ClientId = clientId;
            ClientName = clientName;
            Description = description;
            ClientUri = clientUri;
            LogoUri = logoUri;
            BackChannelLogoutUri = backChannelLogoutUri;
            FrontChannelLogoutUri = frontChannelLogoutUri;
            RequirePkce = requirePkce;
            AllowPlainTextPkce = allowPlainTextPkce;
            AllowAccessTokensViaBrowser = allowAccessTokensViaBrowser;
            AllowOfflineAccess = allowOfflineAccess;
            UpdateAccessTokenClaimsOnRefresh = updateAccessTokenClaimsOnRefresh;
            IncludeJwtId = includeJwtId;
            AlwaysSendClientClaims = alwaysSendClientClaims;
            Enabled = isEnabled;
            RequireClientSecret = requireClientSecret;
            RequireConsent = requireConsent;
            AllowRememberConsent = allowRememberConsent;
            AlwaysIncludeUserClaimsInIdToken = alwaysIncludeUserClaimsInIdToken;
            FrontChannelLogoutSessionRequired = frontChannelLogoutSessionRequired;
            BackChannelLogoutSessionRequired = backChannelLogoutSessionRequired;
            EnableLocalLogin = enableLocalLogin;
            IdentityTokenLifetime = identityTokenLifetime;
            AuthorizationCodeLifetime = authorizationCodeLifetims;
            AbsoluteRefreshTokenLifetime = absoluteRefreshTokenLifetime;
            SlidingRefreshTokenLifetime = slidingRefreshTokenLifetime;
            RefreshTokenUsage = refreshTokenUsage;
            RefreshTokenExpiration = refreshTokenExpiration;
            AccessTokenType = accessTokenType;
            ClientClaimsPrefix = clientClaimsPrefix;
            ProtocolType = protocolType;
            PairWiseSubjectSalt = pairWiseSubjectSalt;
        }

        public void Update(string clientId, string clientName, string description, string clientUri, string logoUri,
            string protocolType = "oidc", string pairWiseSubjectSalt = "")
        {
            ClientId = clientId;
            ClientName = clientName;
            Description = description;
            ClientUri = clientUri;
            LogoUri = logoUri;
            ProtocolType = protocolType;
            PairWiseSubjectSalt = pairWiseSubjectSalt;
        }
        #region Update Methods

        public void UpdateIdentityTokenLifetime(int identityTokenLifetime) => IdentityTokenLifetime = identityTokenLifetime;
        public void UpdatentAccessTokenLifetime(int accessTokenLifetime) => AccessTokenLifetime = accessTokenLifetime;
        public void UpdateAuthorizationCodeLifetime(int authorizationCodeLifetim) => AuthorizationCodeLifetime = authorizationCodeLifetim;
        public void UpdateAbsoluteRefreshTokenLifetime(int absoluteRefreshTokenLifetime) => AbsoluteRefreshTokenLifetime = absoluteRefreshTokenLifetime;
        public void UpdateSlidingRefreshTokenLifetime(int slidingRefreshTokenLifetime) => SlidingRefreshTokenLifetime = slidingRefreshTokenLifetime;
        public void UpdateRefreshTokenUsage(int refreshTokenUsage) => RefreshTokenUsage = refreshTokenUsage;
        public void UpdateRefreshTokenExpiration(int refreshTokenExpiration) => RefreshTokenExpiration = refreshTokenExpiration;
        public void UpdateAccessTokenType(int accessTokenType) => AccessTokenType = accessTokenType;

        public void RequirePkceOn() => RequirePkce = true;
        public void RequirePkceOff() => RequirePkce = false;
        public void AllowPlainTextPkceOn() => AllowPlainTextPkce = true;
        public void AllowPlainTextPkceOff() => AllowPlainTextPkce = false;
        public void AllowAccessTokensViaBrowserOn() => AllowAccessTokensViaBrowser = true;
        public void AllowAccessTokensViaBrowserOff() => AllowAccessTokensViaBrowser = false;

        public void AllowOfflineAccessOn() => AllowOfflineAccess = true;
        public void AllowOfflineAccessOff() => AllowOfflineAccess = false;
        public void UpdateAccessTokenClaimsOnRefreshOn() => UpdateAccessTokenClaimsOnRefresh = true;
        public void UpdateAccessTokenClaimsOnRefreshOff() => UpdateAccessTokenClaimsOnRefresh = false;
        public void IncludeJwtIdOn() => IncludeJwtId = true;
        public void IncludeJwtIdOff() => IncludeJwtId = false;
        public void AlwaysSendClientClaimsOn() => AlwaysSendClientClaims = true;
        public void AlwaysSendClientClaimsOff() => AlwaysSendClientClaims = false;
        public void RequireConsentOn() => RequireConsent = true;
        public void RequireConsentOff() => RequireConsent = false;
        public void AllowRememberConsentOn() => AllowRememberConsent = true;
        public void AllowRememberConsentOff() => AllowRememberConsent = false;
        public void AlwaysIncludeUserClaimsInIdTokenOn() => AlwaysIncludeUserClaimsInIdToken = true;
        public void AlwaysIncludeUserClaimsInIdTokenOff() => AlwaysIncludeUserClaimsInIdToken = false;
        public void FrontChannelLogoutSessionRequiredOn() => FrontChannelLogoutSessionRequired = true;
        public void FrontChannelLogoutSessionRequiredOff() => FrontChannelLogoutSessionRequired = false;
        public void BackChannelLogoutSessionRequiredOn() => BackChannelLogoutSessionRequired = true;
        public void BackChannelLogoutSessionRequiredOff() => BackChannelLogoutSessionRequired = false;
        public void EnableLocalLoginOn() => EnableLocalLogin = true;
        public void EnableLocalLoginOff() => EnableLocalLogin = false;

        public void Enable() => Enabled = true;
        public void Disable() => Enabled = false;
        public void RequireClientSecretOn() => RequireClientSecret = true;
        public void RequireClientSecretOff() => RequireClientSecret = false;
        #endregion

        #region Collections methods
        public void AddClientSecret(ClientSecret secret)
        {
            ClientSecrets.Add(secret);
        }
        public void RemoveClientSecret(ClientSecret secret)
        {
            ClientSecrets.Remove(secret);
        }
        public void ClearClientSecrets()
        {
            ClientSecrets.Clear();
        }
        public void AddClientGrant(ClientGrantType grantType)
        {
            AllowedGrantTypes.Add(grantType);
        }
        public void RemoveClientGrant(ClientGrantType grantType)
        {
            AllowedGrantTypes.Remove(grantType);
        }
        public void ClearClientGrant()
        {
            AllowedGrantTypes.Clear();
        }
        public void AddClientRedirectUri(ClientRedirectUri redirectUri)
        {
            RedirectUris.Add(redirectUri);
        }
        public void RemoveClientRedirectUri(ClientRedirectUri redirectUri)
        {
            RedirectUris.Remove(redirectUri);
        }
        public void ClearClientRedirectUri()
        {
            RedirectUris.Clear();
        }
        public void AddClientPostLogoutRedirectUri(ClientPostLogoutRedirectUri clientPostLogoutRedirectUri)
        {
            PostLogoutRedirectUris.Add(clientPostLogoutRedirectUri);
        }
        public void RemoveClientPostLogoutRedirectUri(ClientPostLogoutRedirectUri clientPostLogoutRedirectUri)
        {
            PostLogoutRedirectUris.Remove(clientPostLogoutRedirectUri);
        }
        public void ClearClientPostLogoutRedirectUri()
        {
            PostLogoutRedirectUris.Clear();
        }
        public void AddClientScope(ClientScope clientScope)
        {
            AllowedScopes.Add(clientScope);
        }
        public void RemoveClientScope(ClientScope clientScope)
        {
            AllowedScopes.Remove(clientScope);
        }
        public void ClearClientScope()
        {
            AllowedScopes.Clear();
        }
        public void AddClientIdPRestriction(ClientIdPRestriction identityProviderRestriction)
        {
            IdentityProviderRestrictions.Add(identityProviderRestriction);
        }
        public void RemoveClientIdPRestriction(ClientIdPRestriction identityProviderRestriction)
        {
            IdentityProviderRestrictions.Remove(identityProviderRestriction);
        }
        public void ClearClientIdPRestriction()
        {
            IdentityProviderRestrictions.Clear();
        }
        public void AddClientClaim(ClientClaim claim)
        {
            Claims.Add(claim);
        }
        public void RemoveClientClaim(ClientClaim claim)
        {
            Claims.Remove(claim);
        }
        public void ClearClientClaim()
        {
            Claims.Clear();
        }
        public void AddClientCorsOrigin(ClientCorsOrigin allowedCorsOrigin)
        {
            AllowedCorsOrigins.Add(allowedCorsOrigin);
        }
        public void RemoveClientCorsOrigin(ClientCorsOrigin allowedCorsOrigin)
        {
            AllowedCorsOrigins.Remove(allowedCorsOrigin);
        }
        public void ClearClientCorsOrigins()
        {
            AllowedCorsOrigins.Clear();
        }
        public void AddClientCorsOrigin(ClientProperty clientProperty)
        {
            Properties.Add(clientProperty);
        }
        public void RemoveClientCorsOrigin(ClientProperty clientProperty)
        {
            Properties.Remove(clientProperty);
        }
        public void ClearClientProperties()
        {
            Properties.Clear();
        }

        #endregion
        
        #region  strings
        public string ClientId { get; set; } //
        public string ClientName { get; set; } //
        public string Description { get; set; } //
        public string ClientUri { get; set; } //
        public string LogoUri { get; set; } //
        public string FrontChannelLogoutUri { get; set; }
        public string BackChannelLogoutUri { get; set; }
        public string PairWiseSubjectSalt { get; set; }
        public string ProtocolType { get; set; } = IdentityServerConstants.ProtocolTypes.OpenIdConnect; //
        public string ClientClaimsPrefix { get; set; } = "client_";
        #endregion

        #region Int
        public int IdentityTokenLifetime { get; set; } = 300;
        public int AccessTokenLifetime { get; set; } = 3600;
        public int AuthorizationCodeLifetime { get; set; } = 300;
        public int? ConsentLifetime { get; set; } = null;
        public int AbsoluteRefreshTokenLifetime { get; set; } = 2592000;
        public int SlidingRefreshTokenLifetime { get; set; } = 1296000;
        public int RefreshTokenUsage { get; set; } = (int)TokenUsage.OneTimeOnly;
        public int RefreshTokenExpiration { get; set; } = (int)TokenExpiration.Absolute;
        public int AccessTokenType { get; set; } = (int)0; // AccessTokenType.Jwt;
        #endregion


        #region Booleans
        public bool Enabled { get; set; } = true; //
        public bool RequireClientSecret { get; set; } = true; //
        public bool RequireConsent { get; set; } = true;
        public bool AllowRememberConsent { get; set; } = true;
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; } = true;
        public bool FrontChannelLogoutSessionRequired { get; set; } = true;
        public bool BackChannelLogoutSessionRequired { get; set; } = true;
        public bool EnableLocalLogin { get; set; } = true;
        public bool AllowOfflineAccess { get; set; }
        public bool RequirePkce { get; set; }
        public bool AllowPlainTextPkce { get; set; }
        public bool AllowAccessTokensViaBrowser { get; set; }
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
        public bool IncludeJwtId { get; set; }
        public bool AlwaysSendClientClaims { get; set; }
        #endregion


        #region Collections
        public virtual ICollection<ClientSecret> ClientSecrets { get; set; } = new HashSet<ClientSecret>();
        public virtual ICollection<ClientGrantType> AllowedGrantTypes { get; set; } = new HashSet<ClientGrantType>();
        public virtual ICollection<ClientRedirectUri> RedirectUris { get; set; } = new HashSet<ClientRedirectUri>();
        public virtual ICollection<ClientPostLogoutRedirectUri> PostLogoutRedirectUris { get; set; } = new HashSet<ClientPostLogoutRedirectUri>();
        public virtual ICollection<ClientScope> AllowedScopes { get; set; } = new HashSet<ClientScope>();
        public virtual ICollection<ClientIdPRestriction> IdentityProviderRestrictions { get; set; } = new HashSet<ClientIdPRestriction>();
        public virtual ICollection<ClientClaim> Claims { get; set; } = new HashSet<ClientClaim>();
        public virtual ICollection<ClientCorsOrigin> AllowedCorsOrigins { get; set; } = new HashSet<ClientCorsOrigin>();
        public virtual ICollection<ClientProperty> Properties { get; set; } = new HashSet<ClientProperty>();
        #endregion
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