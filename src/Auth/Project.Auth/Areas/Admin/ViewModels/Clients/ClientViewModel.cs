using IdentityServer4.EntityFramework.Entities;

namespace Project.Auth.Areas.Admin.ViewModels
{
    public class ClientViewModel
    {
        // public ClientViewModel(string clientId, string clientName, string description, string clientUri, string logoUri, string backChannelLogoutUri, string frontChannelLogoutUri,
        //     bool requirePkce, bool allowPlainTextPkce, bool allowAccessTokensViaBrowser, bool allowOfflineAccess, bool updateAccessTokenClaimsOnRefresh,
        //     bool includeJwtId, bool alwaysSendClientClaims,

        //     bool isEnabled = true, bool requireClientSecret = true, bool requireConsent = true, bool allowRememberConsent = true, bool alwaysIncludeUserClaimsInIdToken = true,
        //     bool frontChannelLogoutSessionRequired = true, bool backChannelLogoutSessionRequired = true, bool enableLocalLogin = true,
            
        //     int identityTokenLifetime = 300, int accessTokenLifetime = 3600, int authorizationCodeLifetims = 300, int absoluteRefreshTokenLifetime = 2592000,
        //     int slidingRefreshTokenLifetime = 1296000, int refreshTokenUsage = (int)IdentityServer4.Models.TokenUsage.OneTimeOnly, int refreshTokenExpiration = (int)IdentityServer4.Models.TokenExpiration.Absolute,
        //     int accessTokenType = (int)0,
        //     string clientClaimsPrefix = "client_", string protocolType = "oidc", string pairWiseSubjectSalt = "")
        // {
        //     ClientId = clientId;
        //     ClientName = clientName;
        //     Description = description;
        //     ClientUri = clientUri;
        //     LogoUri = logoUri;
        //     BackChannelLogoutUri = backChannelLogoutUri;
        //     FrontChannelLogoutUri = frontChannelLogoutUri;
        //     RequirePkce = requirePkce;
        //     AllowPlainTextPkce = allowPlainTextPkce;
        //     AllowAccessTokensViaBrowser = allowAccessTokensViaBrowser;
        //     AllowOfflineAccess = allowOfflineAccess;
        //     UpdateAccessTokenClaimsOnRefresh = updateAccessTokenClaimsOnRefresh;
        //     IncludeJwtId = includeJwtId;
        //     AlwaysSendClientClaims = alwaysSendClientClaims;
        //     Enabled = isEnabled;
        //     RequireClientSecret = requireClientSecret;
        //     RequireConsent = requireConsent;
        //     AllowRememberConsent = allowRememberConsent;
        //     AlwaysIncludeUserClaimsInIdToken = alwaysIncludeUserClaimsInIdToken;
        //     FrontChannelLogoutSessionRequired = frontChannelLogoutSessionRequired;
        //     BackChannelLogoutSessionRequired = backChannelLogoutSessionRequired;
        //     EnableLocalLogin = enableLocalLogin;
        //     IdentityTokenLifetime = identityTokenLifetime;
        //     AuthorizationCodeLifetime = authorizationCodeLifetims;
        //     AbsoluteRefreshTokenLifetime = absoluteRefreshTokenLifetime;
        //     SlidingRefreshTokenLifetime = slidingRefreshTokenLifetime;
        //     RefreshTokenUsage = refreshTokenUsage;
        //     RefreshTokenExpiration = refreshTokenExpiration;
        //     AccessTokenType = accessTokenType;
        //     ClientClaimsPrefix = clientClaimsPrefix;
        //     ProtocolType = protocolType;
        //     PairWiseSubjectSalt = pairWiseSubjectSalt;
        // }

        // public void Update(string clientId, string clientName, string description, string clientUri, string logoUri,
        //     string protocolType = "oidc", string pairWiseSubjectSalt = "")
        // {
        //     ClientId = clientId;
        //     ClientName = clientName;
        //     Description = description;
        //     ClientUri = clientUri;
        //     LogoUri = logoUri;
        //     ProtocolType = protocolType;
        //     PairWiseSubjectSalt = pairWiseSubjectSalt;
        // }
        public int Id { get; set; }
        public int AccessTokenLifetime { get; set; }
        public int AuthorizationCodeLifetime { get; set; }
        public int? ConsentLifetime { get; set; }
        public int AbsoluteRefreshTokenLifetime { get; set; }
        public int SlidingRefreshTokenLifetime { get; set; }
        public int RefreshTokenUsage { get; set; }
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public int AccessTokenType { get; set; }
        public int IdentityTokenLifetime { get; set; }
        public int DeviceCodeLifetime { get; set; }
        public int? UserSsoLifetime { get; set; }


        public bool EnableLocalLogin { get; set; }
        public bool IncludeJwtId { get; set; }
        public bool AlwaysSendClientClaims { get; set; }
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }
        public bool AllowOfflineAccess { get; set; }
        public bool RequireClientSecret { get; set; }
        public bool RequireConsent { get; set; }
        public bool AllowRememberConsent { get; set; }
        public bool RequirePkce { get; set; }
        public bool AllowPlainTextPkce { get; set; }
        public bool RequireRequestObject { get; set; }
        public bool AllowAccessTokensViaBrowser { get; set; }
        public bool Enabled { get; set; }
        public bool FrontChannelLogoutSessionRequired { get; set; }
        public bool BackChannelLogoutSessionRequired { get; set; }
        public bool NonEditable { get; set; }

        public string ClientId { get; set; }
        public string ProtocolType { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
        public string ClientUri { get; set; }
        public string LogoUri { get; set; }
        public string UserCodeType { get; set; }
        public string AllowedIdentityTokenSigningAlgorithms { get; set; }
        public string ClientClaimsPrefix { get; set; }
        public string PairWiseSubjectSalt { get; set; }
        public string FrontChannelLogoutUri { get; set; }
        public string BackChannelLogoutUri { get; set; }


        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? LastAccessed { get; set; }



        public List<ClientRedirectUri> RedirectUris { get; set; }
        public List<ClientPostLogoutRedirectUri> PostLogoutRedirectUris { get; set; }
        public List<ClientGrantType> AllowedGrantTypes { get; set; }
        public List<ClientSecret> ClientSecrets { get; set; }
        public List<ClientScope> AllowedScopes { get; set; }
        public List<ClientCorsOrigin> AllowedCorsOrigins { get; set; }
        public List<ClientProperty> Properties { get; set; }
        public List<ClientClaim> Claims { get; set; }
        public List<ClientIdPRestriction> IdentityProviderRestrictions { get; set; }
    }
}