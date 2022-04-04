using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Project.CMS.Services
{
    public interface ITokenServices
    {
        Task<HttpClient> GetHttpClientAsync(string baseAddress);
    }

    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ILogger<TokenServices> _logger;

        public TokenServices(IConfiguration configuration, IHttpContextAccessor httpContext, ILogger<TokenServices> logger)
        {
            _configuration = configuration;
            _httpContext = httpContext;
            _logger = logger;
        }

        public async Task<HttpClient> GetHttpClientAsync(string baseAddress)
        {
            // var idsClient = new HttpClient();
            // var disco = await idsClient.GetDiscoveryDocumentAsync(_configuration["IdPUrl"]);

            // var tokenResponse = await idsClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            // {
            //     Address = disco.TokenEndpoint,
            //     ClientId = _configuration["ClientId"],
            //     ClientSecret = _configuration["ClientSecret"]
            // });

            // if (tokenResponse.IsError)
            // {
            //     _logger.LogError("Error getting token: {0}", tokenResponse.Error);
            // }

            // var _client = new HttpClient();

            // if(!string.IsNullOrEmpty(tokenResponse.AccessToken))
            // {
            //     _client.SetBearerToken(tokenResponse.AccessToken);
            // }
            // _client.BaseAddress = new Uri(baseAddress);
            // _client.DefaultRequestHeaders.Accept.Clear();
            // _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            // return _client;




            var _client = new HttpClient();

            var currentContext = _httpContext.HttpContext;

            var accessToken = string.Empty;


            var expires_at = await currentContext.GetTokenAsync("expires_at");

            if (string.IsNullOrWhiteSpace(expires_at) || DateTime.Parse(expires_at).AddSeconds(-60).ToUniversalTime() < DateTime.UtcNow)
            {
                Console.WriteLine($"\n\n\n\n\n{DateTime.Parse(expires_at).AddSeconds(-60).ToUniversalTime() < DateTime.UtcNow}\n\n\n\n\n\n");
            }
            else
                accessToken = await currentContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
                accessToken = await RenewTokens();

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                _client.SetBearerToken(accessToken);
            }

            _client.BaseAddress = new Uri(baseAddress);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return _client;
        }







        private async Task<string> RenewTokens()
        {
            var _client = new HttpClient();

            var currentContext = _httpContext.HttpContext;

            var idpUrl = _configuration["IdPUrl"];
            var clientId = _configuration["ClientId"];
            var clientSecret = _configuration["ClientSecret"];

            var discoveryDocument = await _client.GetDiscoveryDocumentAsync(idpUrl);
            var tokenEndpoint = discoveryDocument.TokenEndpoint;

            var currentRefreshToken = await currentContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            Console.WriteLine($"\n\n\n\n\nRefresh Token: {currentRefreshToken}\n\n\n\n");


            TokenResponse tokenRespons = await _client.RequestRefreshTokenAsync(new RefreshTokenRequest
            {
                Address = tokenEndpoint,
                ClientId = clientId,
                ClientSecret = clientSecret,
                RefreshToken = currentRefreshToken,
                GrantType = OpenIdConnectGrantTypes.RefreshToken
            });

            if (tokenRespons.IsError)
            {
                throw new Exception($"Error: {tokenRespons.Error}");
            }
            var updatedTokens = new List<AuthenticationToken>();
            updatedTokens.Add(new AuthenticationToken
            {
                Name = OpenIdConnectParameterNames.IdToken,
                Value = tokenRespons.IdentityToken
            });
            updatedTokens.Add(new AuthenticationToken
            {
                Name = OpenIdConnectParameterNames.AccessToken,
                Value = tokenRespons.AccessToken
            });

            Console.WriteLine($"\n\n\n\n\n New RefreshToken: {tokenRespons.RefreshToken}\n\n\n\n\n\n");

            updatedTokens.Add(new AuthenticationToken
            {
                Name = OpenIdConnectParameterNames.RefreshToken,
                Value = tokenRespons.RefreshToken
            });

            var expiresAt = DateTime.UtcNow + TimeSpan.FromSeconds(tokenRespons.ExpiresIn);
            updatedTokens.Add(new AuthenticationToken
            {
                Name = "expires_at",
                Value = expiresAt.ToString("o", CultureInfo.InvariantCulture)
            });

            // get authenticate result, containing the current principal & properties
            var currentAuthenticateResult = await currentContext.AuthenticateAsync("Cookies");

            // store the updated tokens
            currentAuthenticateResult.Properties.StoreTokens(updatedTokens);

            // sign in
            await currentContext.SignInAsync("Cookies",
             currentAuthenticateResult.Principal, currentAuthenticateResult.Properties);

            // return the new access token
            //return result.AccessToken;
            return tokenRespons.AccessToken;
        }

    }
}