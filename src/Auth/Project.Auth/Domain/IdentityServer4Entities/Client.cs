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
        private Client()
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
            _clientSecrets.Add(secret);
        }
        public void RemoveClientSecret(ClientSecret secret)
        {
            _clientSecrets.Remove(secret);
        }
        public void ClearClientSecrets()
        {
            _clientSecrets.Clear();
        }
        public void AddClientGrant(ClientGrantType grantType)
        {
            _allowedGrantTypes.Add(grantType);
        }
        public void RemoveClientGrant(ClientGrantType grantType)
        {
            _allowedGrantTypes.Remove(grantType);
        }
        public void ClearClientGrant()
        {
            _allowedGrantTypes.Clear();
        }
        public void AddClientRedirectUri(ClientRedirectUri redirectUri)
        {
            _redirectUris.Add(redirectUri);
        }
        public void RemoveClientRedirectUri(ClientRedirectUri redirectUri)
        {
            _redirectUris.Remove(redirectUri);
        }
        public void ClearClientRedirectUri()
        {
            _redirectUris.Clear();
        }
        public void AddClientPostLogoutRedirectUri(ClientPostLogoutRedirectUri clientPostLogoutRedirectUri)
        {
            _postLogoutRedirectUris.Add(clientPostLogoutRedirectUri);
        }
        public void RemoveClientPostLogoutRedirectUri(ClientPostLogoutRedirectUri clientPostLogoutRedirectUri)
        {
            _postLogoutRedirectUris.Remove(clientPostLogoutRedirectUri);
        }
        public void ClearClientPostLogoutRedirectUri()
        {
            _postLogoutRedirectUris.Clear();
        }
        public void AddClientScope(ClientScope clientScope)
        {
            _allowedScopes.Add(clientScope);
        }
        public void RemoveClientScope(ClientScope clientScope)
        {
            _allowedScopes.Remove(clientScope);
        }
        public void ClearClientScope()
        {
            _allowedScopes.Clear();
        }
        public void AddClientIdPRestriction(ClientIdPRestriction identityProviderRestriction)
        {
            _identityProviderRestrictions.Add(identityProviderRestriction);
        }
        public void RemoveClientIdPRestriction(ClientIdPRestriction identityProviderRestriction)
        {
            _identityProviderRestrictions.Remove(identityProviderRestriction);
        }
        public void ClearClientIdPRestriction()
        {
            _identityProviderRestrictions.Clear();
        }
        public void AddClientClaim(ClientClaim claim)
        {
            _claims.Add(claim);
        }
        public void RemoveClientClaim(ClientClaim claim)
        {
            _claims.Remove(claim);
        }
        public void ClearClientClaim()
        {
            _claims.Clear();
        }
        public void AddClientCorsOrigin(ClientCorsOrigin allowedCorsOrigin)
        {
            _allowedCorsOrigins.Add(allowedCorsOrigin);
        }
        public void RemoveClientCorsOrigin(ClientCorsOrigin allowedCorsOrigin)
        {
            _allowedCorsOrigins.Remove(allowedCorsOrigin);
        }
        public void ClearClientCorsOrigins()
        {
            _allowedCorsOrigins.Clear();
        }
        public void AddClientCorsOrigin(ClientProperty clientProperty)
        {
            _properties.Add(clientProperty);
        }
        public void RemoveClientCorsOrigin(ClientProperty clientProperty)
        {
            _properties.Remove(clientProperty);
        }
        public void ClearClientProperties()
        {
            _properties.Clear();
        }

        #endregion
        
        #region  strings
        public string ClientId { get; private set; } //
        public string ClientName { get; private set; } //
        public string Description { get; private set; } //
        public string ClientUri { get; private set; } //
        public string LogoUri { get; private set; } //
        public string FrontChannelLogoutUri { get; private set; }
        public string BackChannelLogoutUri { get; private set; }
        public string PairWiseSubjectSalt { get; private set; }
        public string ProtocolType { get; private set; } = IdentityServerConstants.ProtocolTypes.OpenIdConnect; //
        public string ClientClaimsPrefix { get; private set; } = "client_";
        #endregion

        #region Int
        public int IdentityTokenLifetime { get; private set; } = 300;
        public int AccessTokenLifetime { get; private set; } = 3600;
        public int AuthorizationCodeLifetime { get; private set; } = 300;
        public int? ConsentLifetime { get; private set; } = null;
        public int AbsoluteRefreshTokenLifetime { get; private set; } = 2592000;
        public int SlidingRefreshTokenLifetime { get; private set; } = 1296000;
        public int RefreshTokenUsage { get; private set; } = (int)TokenUsage.OneTimeOnly;
        public int RefreshTokenExpiration { get; private set; } = (int)TokenExpiration.Absolute;
        public int AccessTokenType { get; private set; } = (int)0; // AccessTokenType.Jwt;
        #endregion


        #region Booleans
        public bool Enabled { get; private set; } = true; //
        public bool RequireClientSecret { get; private set; } = true; //
        public bool RequireConsent { get; private set; } = true;
        public bool AllowRememberConsent { get; private set; } = true;
        public bool AlwaysIncludeUserClaimsInIdToken { get; private set; } = true;
        public bool FrontChannelLogoutSessionRequired { get; private set; } = true;
        public bool BackChannelLogoutSessionRequired { get; private set; } = true;
        public bool EnableLocalLogin { get; private set; } = true;
        public bool AllowOfflineAccess { get; private set; }
        public bool RequirePkce { get; private set; }
        public bool AllowPlainTextPkce { get; private set; }
        public bool AllowAccessTokensViaBrowser { get; private set; }
        public bool UpdateAccessTokenClaimsOnRefresh { get; private set; }
        public bool IncludeJwtId { get; private set; }
        public bool AlwaysSendClientClaims { get; private set; }
        #endregion


        #region Collections
        private List<ClientSecret> _clientSecrets = new List<ClientSecret>();
        public IReadOnlyCollection<ClientSecret> ClientSecrets
        {
            get { return _clientSecrets.AsReadOnly(); }
        }

        private List<ClientGrantType> _allowedGrantTypes = new List<ClientGrantType>();
        public IReadOnlyCollection<ClientGrantType> AllowedGrantTypes
        {
            get { return _allowedGrantTypes.AsReadOnly(); }
        }

        private List<ClientRedirectUri> _redirectUris = new List<ClientRedirectUri>();
        public IReadOnlyCollection<ClientRedirectUri> RedirectUris
        {
            get { return _redirectUris.AsReadOnly(); }
        }
        private List<ClientPostLogoutRedirectUri> _postLogoutRedirectUris = new List<ClientPostLogoutRedirectUri>();
        public IReadOnlyCollection<ClientPostLogoutRedirectUri> PostLogoutRedirectUris
        {
            get { return _postLogoutRedirectUris.AsReadOnly(); }
        }
        private List<ClientScope> _allowedScopes = new List<ClientScope>();
        public IReadOnlyCollection<ClientScope> AllowedScopes
        {
            get { return _allowedScopes.AsReadOnly(); }
        }
        private List<ClientIdPRestriction> _identityProviderRestrictions = new List<ClientIdPRestriction>();
        public IReadOnlyCollection<ClientIdPRestriction> IdentityProviderRestrictions
        {
            get { return _identityProviderRestrictions.AsReadOnly(); }
        }
        private List<ClientClaim> _claims = new List<ClientClaim>();
        public IReadOnlyCollection<ClientClaim> Claims
        {
            get { return _claims.AsReadOnly(); }
        }
        private List<ClientCorsOrigin> _allowedCorsOrigins = new List<ClientCorsOrigin>();
        public IReadOnlyCollection<ClientCorsOrigin> AllowedCorsOrigins
        {
            get { return _allowedCorsOrigins.AsReadOnly(); }
        }
        private List<ClientProperty> _properties= new List<ClientProperty>();
        public IReadOnlyCollection<ClientProperty> Properties
        {
            get { return _properties.AsReadOnly(); }
        }
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