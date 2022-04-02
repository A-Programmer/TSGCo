using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Project.Auth.Data;
using Project.Auth.Models;

namespace Project.Auth.Services
{
    public interface IClientServices
    {
        Task<ResultMessage> UpdateClient(Client client);
        Task<ResultMessage> AddClient(Client client);
        IQueryable<Client> GetQueryable();
        Task<Client> GetById(int id);
        Task<ResultMessage> Delete(int id);


        Task<List<ClientSecret>> GetClientSecretsAsync(int clientId);
        Task<ResultMessage> AddClientSecret(ClientSecret secret);
        Task<ResultMessage> DeleteSecret(int id);
        Task<ResultMessage> RemoveAllClientSecrets(int clientId);


        Task<List<ClientRedirectUri>> GetClientRedirectUrisAsync(int clientId);
        Task<ResultMessage> AddRedirectUri(ClientRedirectUri redirectUri);
        Task<ResultMessage> DeleteClientRedirectUri(int id);
        Task<ResultMessage> RemoveAllClientRedirectUris(int clientId);


        Task<List<ClientPostLogoutRedirectUri>> GetPostLogoutRedirectUrisAsync(int clientId);
        Task<ResultMessage> AddPostLogoutRedirectUri(ClientPostLogoutRedirectUri postLogoutRedirectUri);
        Task<ResultMessage> DeleteClientPostLogoutRedirectUri(int id);
        Task<ResultMessage> RemoveAllPostLogoutRedirectUris(int clientId);


        Task<List<ClientGrantType>> GetClientGrantTypesAsync(int clientId);
        Task<ResultMessage> AddClientGrantType(ClientGrantType grantType);
        Task<ResultMessage> DeleteClientGrantType(int id);
        Task<ResultMessage> RemoveAllClientGrantTypes(int clientId);


        Task<List<ClientScope>> GetClientScopesAsync(int clientId);
        Task<ResultMessage> AddClientScope(ClientScope scope);
        Task<ResultMessage> DeleteClientScope(int id);
        Task<ResultMessage> RemoveAllClientScopes(int clientId);


        Task<List<ClientCorsOrigin>> GetClientCorsOriginsAsync(int clientId);
        Task<ResultMessage> AddCorsOrigin(ClientCorsOrigin corsOrigin);
        Task<ResultMessage> DeleteClientCorsOrigin(int id);
        Task<ResultMessage> RemoveAllClientCorsOrigins(int clientId);


        Task<List<ClientProperty>> GetClientPropertiesAsync(int clientId);
        Task<ResultMessage> AddClientProperty(ClientProperty property);
        Task<ResultMessage> DeleteClientProperty(int id);
        Task<ResultMessage> RemoveAllClientProperties(int clientId);


        Task<List<ClientClaim>> GetClientClaimsAsync(int clientId);
        Task<ResultMessage> AddClientClaim(ClientClaim claim);
        Task<ResultMessage> DeleteClientClaim(int id);
        Task<ResultMessage> RemoveAllClientClaims(int clientId);


        Task<List<ClientIdPRestriction>> GetClientIdPRestrictionsAsync(int clientId);
        Task<ResultMessage> AddClientIdPRestriction(ClientIdPRestriction idpRestriction);
        Task<ResultMessage> DeleteClientIdPRestriction(int id);
        Task<ResultMessage> RemoveAllClientIdPRestrictions(int clientId);
    }



    public class ClientServices : IClientServices
    {
        private readonly IUnitOfWork _uow;
        private readonly ApplicationDbContext _context;

        public ClientServices(IUnitOfWork uow, ApplicationDbContext context)
        {
            _uow = uow;
            _context = context;
        }

        public async Task<ResultMessage> UpdateClient(Client client)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                var currentClient = await _uow.Set<Client>().FirstOrDefaultAsync(x => x.Id == client.Id);
                if(currentClient != null)
                {
                    currentClient.AccessTokenLifetime = client.AccessTokenLifetime;
                    currentClient.AuthorizationCodeLifetime = client.AuthorizationCodeLifetime;
                    currentClient.ConsentLifetime = client.ConsentLifetime;
                    currentClient.AbsoluteRefreshTokenLifetime = client.AbsoluteRefreshTokenLifetime;
                    currentClient.SlidingRefreshTokenLifetime = client.SlidingRefreshTokenLifetime;
                    currentClient.RefreshTokenUsage = client.RefreshTokenUsage;
                    currentClient.UpdateAccessTokenClaimsOnRefresh = client.UpdateAccessTokenClaimsOnRefresh;
                    currentClient.RefreshTokenExpiration = client.RefreshTokenExpiration;
                    currentClient.AccessTokenType = client.AccessTokenType;
                    currentClient.IdentityTokenLifetime = client.IdentityTokenLifetime;
                    currentClient.DeviceCodeLifetime = client.DeviceCodeLifetime;
                    currentClient.UserSsoLifetime = client.UserSsoLifetime;

                    currentClient.EnableLocalLogin = client.EnableLocalLogin;
                    currentClient.IncludeJwtId = client.IncludeJwtId;
                    currentClient.AlwaysSendClientClaims = client.AlwaysSendClientClaims;
                    currentClient.AlwaysIncludeUserClaimsInIdToken = client.AlwaysIncludeUserClaimsInIdToken;
                    currentClient.AllowOfflineAccess = client.AllowOfflineAccess;
                    currentClient.RequireClientSecret = client.RequireClientSecret;
                    currentClient.RequireConsent = client.RequireConsent;
                    currentClient.AllowRememberConsent = client.AllowRememberConsent;
                    currentClient.RequirePkce = client.RequirePkce;
                    currentClient.AllowPlainTextPkce = client.AllowPlainTextPkce;
                    currentClient.RequireRequestObject = client.RequireRequestObject;
                    currentClient.AllowAccessTokensViaBrowser = client.AllowAccessTokensViaBrowser;
                    currentClient.Enabled = client.Enabled;
                    currentClient.FrontChannelLogoutSessionRequired = client.FrontChannelLogoutSessionRequired;
                    currentClient.BackChannelLogoutSessionRequired = client.BackChannelLogoutSessionRequired;
                    currentClient.NonEditable = client.NonEditable;

                    currentClient.ClientId = client.ClientId;
                    currentClient.ProtocolType = client.ProtocolType;
                    currentClient.ClientName = client.ClientName;
                    currentClient.Description = client.Description;
                    currentClient.ClientUri = client.ClientUri;
                    currentClient.LogoUri = client.LogoUri;
                    currentClient.UserCodeType = client.UserCodeType;
                    currentClient.AllowedIdentityTokenSigningAlgorithms = client.AllowedIdentityTokenSigningAlgorithms;
                    currentClient.ClientClaimsPrefix = client.ClientClaimsPrefix;
                    currentClient.PairWiseSubjectSalt = client.PairWiseSubjectSalt;
                    currentClient.FrontChannelLogoutUri = client.FrontChannelLogoutUri;
                    currentClient.BackChannelLogoutUri = client.BackChannelLogoutUri;


                    if(client.AccessTokenLifetime == 0)
                        currentClient.AccessTokenLifetime = 3600;
                        
                    if(client.IdentityTokenLifetime == 0)
                        currentClient.IdentityTokenLifetime = 300;

                    if(client.AuthorizationCodeLifetime == 0)
                        currentClient.AuthorizationCodeLifetime = 300;

                    if(client.AbsoluteRefreshTokenLifetime == 0)
                        currentClient.AbsoluteRefreshTokenLifetime = 2592000;

                    if(client.SlidingRefreshTokenLifetime == 0)
                        currentClient.SlidingRefreshTokenLifetime = 1296000;

                    if(client.RefreshTokenUsage == 0)
                        currentClient.RefreshTokenUsage = (int)IdentityServer4.Models.TokenUsage.OneTimeOnly;

                    if(client.RefreshTokenExpiration == 0)
                        currentClient.RefreshTokenExpiration = (int)IdentityServer4.Models.TokenExpiration.Absolute;



                    await _uow.SaveChangesAsync();
                    result.Update(true, "success", "کلاینت مورد نظر با موفقیت ویرایش شد.");
                }
                else
                {
                    result.Update(false, "warning", "کلاینت مورد نظر یافت نشد.");
                }
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در ویرایش کلاینت: " + ex.Message);
            }
            return result;
        }

        public async Task<ResultMessage> AddClient(Client client)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                if(client.AccessTokenLifetime == 0)
                    client.AccessTokenLifetime = 3600;
                    
                if(client.IdentityTokenLifetime == 0)
                    client.IdentityTokenLifetime = 300;

                if(client.AuthorizationCodeLifetime == 0)
                    client.AuthorizationCodeLifetime = 300;

                if(client.AbsoluteRefreshTokenLifetime == 0)
                    client.AbsoluteRefreshTokenLifetime = 2592000;

                if(client.SlidingRefreshTokenLifetime == 0)
                    client.SlidingRefreshTokenLifetime = 1296000;

                if(client.ConsentLifetime == 0)
                    client.ConsentLifetime = 3600;

                if(client.RefreshTokenUsage == 0)
                    client.RefreshTokenUsage = (int)IdentityServer4.Models.TokenUsage.OneTimeOnly;

                if(client.RefreshTokenExpiration == 0)
                    client.RefreshTokenExpiration = (int)IdentityServer4.Models.TokenExpiration.Absolute;


                if(string.IsNullOrEmpty(client.ProtocolType))
                    client.ProtocolType = "iodc";

                await _uow.Set<Client>()
                    .AddAsync(client);
                await _uow.SaveChangesAsync();
                result.Update(true, "success", "کلاینت مورد نظر با موفقیت ثبت شد.");
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در ثبت کلاینت: " + ex.Message);
            }
            return result;
        }

        public IQueryable<Client> GetQueryable()
        {
            return _uow.Set<Client>().AsQueryable();
        }

        public async Task<Client> GetById(int id)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ResultMessage> Delete(int id)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                var client = await _uow.Set<Client>()
                    .FirstOrDefaultAsync(x => x.Id == id);
                if(client == null)
                {
                    result.Update(false, "warning", "کلاینت مورد نظر یافت نشد.");
                }
                else
                {
                    //Remove Client List Members
                    //..
                    //..
                    _uow.Set<Client>().Remove(client);
                    await _uow.SaveChangesAsync();
                    result.Update(true, "success", "کلاینت مورد نظر با موفقیت حذف شد.");
                }
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در حذف کلاینت: " + ex.Message);
            }
            return result;
        }
        
        #region Client Secrets
        public async Task<List<ClientSecret>> GetClientSecretsAsync(int clientId)
        {
            return await _uow.Set<ClientSecret>()
                .Where(x => x.ClientId == clientId)
                .ToListAsync();
        }
        public async Task<ResultMessage> AddClientSecret(ClientSecret secret)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                await _uow.Set<ClientSecret>()
                    .AddAsync(secret);
                await _uow.SaveChangesAsync();
                result.Update(true, "success", "کلمه عبور با موفقیت ثبت شد.");
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در ثبت کلمه عبور: " + ex.Message);
            }
            return result;
        }
        public async Task<ResultMessage> RemoveAllClientSecrets(int clientId)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                var clientSecrets = await _uow.Set<ClientSecret>()
                    .Where(x => x.ClientId == clientId).ToListAsync();
                foreach(var secret in clientSecrets)
                {
                    _uow.Set<ClientSecret>()
                        .Remove(secret);
                }
                
                await _uow.SaveChangesAsync();

                result.Update(true, "success", "کلمات عبور با موفقیت حذف شدند.");
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در حذف کلمات عبور: " + ex.Message);
            }
            return result;
        }
        public async Task<ResultMessage> DeleteSecret(int id)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                var client = await _uow.Set<ClientSecret>()
                    .FirstOrDefaultAsync(x => x.Id == id);
                if(client == null)
                {
                    result.Update(false, "warning", "رکورد مورد نظر یافت نشد.");
                }
                else
                {
                    _uow.Set<ClientSecret>().Remove(client);
                    await _uow.SaveChangesAsync();
                    result.Update(true, "success", "رکورد مورد نظر با موفقیت حذف شد.");
                }
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در حذف رکورد: " + ex.Message);
            }
            return result;
        }
        
        #endregion
        
        #region Client Redirect Uris
        public async Task<List<ClientRedirectUri>> GetClientRedirectUrisAsync(int clientId)
        {
            return await _uow.Set<ClientRedirectUri>()
                .Where(x => x.ClientId == clientId)
                .ToListAsync();
        }
        public async Task<ResultMessage> AddRedirectUri(ClientRedirectUri redirectUri)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                await _uow.Set<ClientRedirectUri>()
                    .AddAsync(redirectUri);
                await _uow.SaveChangesAsync();
                result.Update(true, "success", "عملیات با موفقیت انجام شد.");
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در عملیات ثبت: " + ex.Message);
            }
            return result;
        }
        public async Task<ResultMessage> RemoveAllClientRedirectUris(int clientId)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                var entities = await _uow.Set<ClientRedirectUri>()
                    .Where(x => x.ClientId == clientId).ToListAsync();
                foreach(var entity in entities)
                {
                    _uow.Set<ClientRedirectUri>()
                        .Remove(entity);
                }
                
                await _uow.SaveChangesAsync();

                result.Update(true, "success", "رکورد مورد نظر با موفقبت حذف شد.");
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در عملیات حذف: " + ex.Message);
            }
            return result;
        }
        public async Task<ResultMessage> DeleteClientRedirectUri(int id)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                var client = await _uow.Set<ClientRedirectUri>()
                    .FirstOrDefaultAsync(x => x.Id == id);
                if(client == null)
                {
                    result.Update(false, "warning", "رکورد مورد نظر یافت نشد.");
                }
                else
                {
                    _uow.Set<ClientRedirectUri>().Remove(client);
                    await _uow.SaveChangesAsync();
                    result.Update(true, "success", "رکورد مورد نظر با موفقیت حذف شد.");
                }
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در حذف رکورد: " + ex.Message);
            }
            return result;
        }
        #endregion
        
        #region Client Post Logout Redirect Uris
        public async Task<List<ClientPostLogoutRedirectUri>> GetPostLogoutRedirectUrisAsync(int clientId)
        {
            return await _uow.Set<ClientPostLogoutRedirectUri>()
                .Where(x => x.ClientId == clientId)
                .ToListAsync();
        }
        public async Task<ResultMessage> AddPostLogoutRedirectUri(ClientPostLogoutRedirectUri postLogoutRedirectUri)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                await _uow.Set<ClientPostLogoutRedirectUri>()
                    .AddAsync(postLogoutRedirectUri);
                await _uow.SaveChangesAsync();
                result.Update(true, "success", "عملیات با موفقیت انجام شد.");
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در عملیات ثبت: " + ex.Message);
            }
            return result;
        }
        public async Task<ResultMessage> RemoveAllPostLogoutRedirectUris(int clientId)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                var entities = await _uow.Set<ClientPostLogoutRedirectUri>()
                    .Where(x => x.ClientId == clientId).ToListAsync();
                foreach(var entity in entities)
                {
                    _uow.Set<ClientPostLogoutRedirectUri>()
                        .Remove(entity);
                }
                
                await _uow.SaveChangesAsync();

                result.Update(true, "success", "رکورد مورد نظر با موفقبت حذف شد.");
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در عملیات حذف: " + ex.Message);
            }
            return result;
        }
        public async Task<ResultMessage> DeleteClientPostLogoutRedirectUri(int id)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                var client = await _uow.Set<ClientPostLogoutRedirectUri>()
                    .FirstOrDefaultAsync(x => x.Id == id);
                if(client == null)
                {
                    result.Update(false, "warning", "رکورد مورد نظر یافت نشد.");
                }
                else
                {
                    _uow.Set<ClientPostLogoutRedirectUri>().Remove(client);
                    await _uow.SaveChangesAsync();
                    result.Update(true, "success", "رکورد مورد نظر با موفقیت حذف شد.");
                }
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در حذف رکورد: " + ex.Message);
            }
            return result;
        }
        #endregion

        #region Client Grant Types
        public async Task<List<ClientGrantType>> GetClientGrantTypesAsync(int clientId)
        {
            return await _uow.Set<ClientGrantType>()
                .Where(x => x.ClientId == clientId)
                .ToListAsync();
        }
        public async Task<ResultMessage> AddClientGrantType(ClientGrantType grantType)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                await _uow.Set<ClientGrantType>()
                    .AddAsync(grantType);
                await _uow.SaveChangesAsync();
                result.Update(true, "success", "عملیات با موفقیت انجام شد.");
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در عملیات ثبت: " + ex.Message);
            }
            return result;
        }
        public async Task<ResultMessage> RemoveAllClientGrantTypes(int clientId)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                var entities = await _uow.Set<ClientGrantType>()
                    .Where(x => x.ClientId == clientId).ToListAsync();
                foreach(var entity in entities)
                {
                    _uow.Set<ClientGrantType>()
                        .Remove(entity);
                }
                
                await _uow.SaveChangesAsync();

                result.Update(true, "success", "رکورد مورد نظر با موفقبت حذف شد.");
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در عملیات حذف: " + ex.Message);
            }
            return result;
        }
        public async Task<ResultMessage> DeleteClientGrantType(int id)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                var client = await _uow.Set<ClientGrantType>()
                    .FirstOrDefaultAsync(x => x.Id == id);
                if(client == null)
                {
                    result.Update(false, "warning", "رکورد مورد نظر یافت نشد.");
                }
                else
                {
                    _uow.Set<ClientGrantType>().Remove(client);
                    await _uow.SaveChangesAsync();
                    result.Update(true, "success", "رکورد مورد نظر با موفقیت حذف شد.");
                }
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در حذف رکورد: " + ex.Message);
            }
            return result;
        }
        #endregion

        #region Client Scopes
        public async Task<List<ClientScope>> GetClientScopesAsync(int clientId)
        {
            return await _uow.Set<ClientScope>()
                .Where(x => x.ClientId == clientId)
                .ToListAsync();
        }
        public async Task<ResultMessage> AddClientScope(ClientScope scope)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                await _uow.Set<ClientScope>()
                    .AddAsync(scope);
                await _uow.SaveChangesAsync();
                result.Update(true, "success", "عملیات با موفقیت انجام شد.");
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در عملیات ثبت: " + ex.Message);
            }
            return result;
        }
        public async Task<ResultMessage> RemoveAllClientScopes(int clientId)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                var entities = await _uow.Set<ClientScope>()
                    .Where(x => x.ClientId == clientId).ToListAsync();
                foreach(var entity in entities)
                {
                    _uow.Set<ClientScope>()
                        .Remove(entity);
                }
                
                await _uow.SaveChangesAsync();

                result.Update(true, "success", "رکورد مورد نظر با موفقبت حذف شد.");
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در عملیات حذف: " + ex.Message);
            }
            return result;
        }
        public async Task<ResultMessage> DeleteClientScope(int id)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                var client = await _uow.Set<ClientScope>()
                    .FirstOrDefaultAsync(x => x.Id == id);
                if(client == null)
                {
                    result.Update(false, "warning", "رکورد مورد نظر یافت نشد.");
                }
                else
                {
                    _uow.Set<ClientScope>().Remove(client);
                    await _uow.SaveChangesAsync();
                    result.Update(true, "success", "رکورد مورد نظر با موفقیت حذف شد.");
                }
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در حذف رکورد: " + ex.Message);
            }
            return result;
        }
        #endregion

        #region Client Cors Origins
        public async Task<List<ClientCorsOrigin>> GetClientCorsOriginsAsync(int clientId)
        {
            return await _uow.Set<ClientCorsOrigin>()
                .Where(x => x.ClientId == clientId)
                .ToListAsync();
        }
        public async Task<ResultMessage> AddCorsOrigin(ClientCorsOrigin corsOrigin)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                await _uow.Set<ClientCorsOrigin>()
                    .AddAsync(corsOrigin);
                await _uow.SaveChangesAsync();
                result.Update(true, "success", "عملیات با موفقیت انجام شد.");
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در عملیات ثبت: " + ex.Message);
            }
            return result;
        }
        public async Task<ResultMessage> RemoveAllClientCorsOrigins(int clientId)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                var entities = await _uow.Set<ClientCorsOrigin>()
                    .Where(x => x.ClientId == clientId).ToListAsync();
                foreach(var entity in entities)
                {
                    _uow.Set<ClientCorsOrigin>()
                        .Remove(entity);
                }
                
                await _uow.SaveChangesAsync();

                result.Update(true, "success", "رکورد مورد نظر با موفقبت حذف شد.");
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در عملیات حذف: " + ex.Message);
            }
            return result;
        }
        public async Task<ResultMessage> DeleteClientCorsOrigin(int id)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                var client = await _uow.Set<ClientCorsOrigin>()
                    .FirstOrDefaultAsync(x => x.Id == id);
                if(client == null)
                {
                    result.Update(false, "warning", "رکورد مورد نظر یافت نشد.");
                }
                else
                {
                    _uow.Set<ClientCorsOrigin>().Remove(client);
                    await _uow.SaveChangesAsync();
                    result.Update(true, "success", "رکورد مورد نظر با موفقیت حذف شد.");
                }
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در حذف رکورد: " + ex.Message);
            }
            return result;
        }
        #endregion

        #region Client Properties
        public async Task<List<ClientProperty>> GetClientPropertiesAsync(int clientId)
        {
            return await _uow.Set<ClientProperty>()
                .Where(x => x.ClientId == clientId)
                .ToListAsync();
        }
        public async Task<ResultMessage> AddClientProperty(ClientProperty property)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                await _uow.Set<ClientProperty>()
                    .AddAsync(property);
                await _uow.SaveChangesAsync();
                result.Update(true, "success", "عملیات با موفقیت انجام شد.");
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در عملیات ثبت: " + ex.Message);
            }
            return result;
        }
        public async Task<ResultMessage> RemoveAllClientProperties(int clientId)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                var entities = await _uow.Set<ClientProperty>()
                    .Where(x => x.ClientId == clientId).ToListAsync();
                foreach(var entity in entities)
                {
                    _uow.Set<ClientProperty>()
                        .Remove(entity);
                }
                
                await _uow.SaveChangesAsync();

                result.Update(true, "success", "رکورد مورد نظر با موفقبت حذف شد.");
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در عملیات حذف: " + ex.Message);
            }
            return result;
        }
        public async Task<ResultMessage> DeleteClientProperty(int id)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                var client = await _uow.Set<ClientProperty>()
                    .FirstOrDefaultAsync(x => x.Id == id);
                if(client == null)
                {
                    result.Update(false, "warning", "رکورد مورد نظر یافت نشد.");
                }
                else
                {
                    _uow.Set<ClientProperty>().Remove(client);
                    await _uow.SaveChangesAsync();
                    result.Update(true, "success", "رکورد مورد نظر با موفقیت حذف شد.");
                }
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در حذف رکورد: " + ex.Message);
            }
            return result;
        }
        #endregion

        #region Client Claims
        public async Task<List<ClientClaim>> GetClientClaimsAsync(int clientId)
        {
            return await _uow.Set<ClientClaim>()
                .Where(x => x.ClientId == clientId)
                .ToListAsync();
        }
        public async Task<ResultMessage> AddClientClaim(ClientClaim claim)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                await _uow.Set<ClientClaim>()
                    .AddAsync(claim);
                await _uow.SaveChangesAsync();
                result.Update(true, "success", "عملیات با موفقیت انجام شد.");
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در عملیات ثبت: " + ex.Message);
            }
            return result;
        }
        public async Task<ResultMessage> RemoveAllClientClaims(int clientId)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                var entities = await _uow.Set<ClientClaim>()
                    .Where(x => x.ClientId == clientId).ToListAsync();
                foreach(var entity in entities)
                {
                    _uow.Set<ClientClaim>()
                        .Remove(entity);
                }
                
                await _uow.SaveChangesAsync();

                result.Update(true, "success", "رکورد مورد نظر با موفقبت حذف شد.");
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در عملیات حذف: " + ex.Message);
            }
            return result;
        }
        public async Task<ResultMessage> DeleteClientClaim(int id)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                var client = await _uow.Set<ClientClaim>()
                    .FirstOrDefaultAsync(x => x.Id == id);
                if(client == null)
                {
                    result.Update(false, "warning", "رکورد مورد نظر یافت نشد.");
                }
                else
                {
                    _uow.Set<ClientClaim>().Remove(client);
                    await _uow.SaveChangesAsync();
                    result.Update(true, "success", "رکورد مورد نظر با موفقیت حذف شد.");
                }
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در حذف رکورد: " + ex.Message);
            }
            return result;
        }
        #endregion

        #region Client IdP Restrictions
        public async Task<List<ClientIdPRestriction>> GetClientIdPRestrictionsAsync(int clientId)
        {
            return await _uow.Set<ClientIdPRestriction>()
                .Where(x => x.ClientId == clientId)
                .ToListAsync();
        }
        public async Task<ResultMessage> AddClientIdPRestriction(ClientIdPRestriction idpRestriction)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                await _uow.Set<ClientIdPRestriction>()
                    .AddAsync(idpRestriction);
                await _uow.SaveChangesAsync();
                result.Update(true, "success", "عملیات با موفقیت انجام شد.");
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در عملیات ثبت: " + ex.Message);
            }
            return result;
        }
        public async Task<ResultMessage> RemoveAllClientIdPRestrictions(int clientId)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                var entities = await _uow.Set<ClientIdPRestriction>()
                    .Where(x => x.ClientId == clientId).ToListAsync();
                foreach(var entity in entities)
                {
                    _uow.Set<ClientIdPRestriction>()
                        .Remove(entity);
                }
                
                await _uow.SaveChangesAsync();

                result.Update(true, "success", "رکورد مورد نظر با موفقبت حذف شد.");
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در عملیات حذف: " + ex.Message);
            }
            return result;
        }
        public async Task<ResultMessage> DeleteClientIdPRestriction(int id)
        {
            var result = new ResultMessage(false, "warning", "");

            try
            {
                var client = await _uow.Set<ClientIdPRestriction>()
                    .FirstOrDefaultAsync(x => x.Id == id);
                if(client == null)
                {
                    result.Update(false, "warning", "رکورد مورد نظر یافت نشد.");
                }
                else
                {
                    _uow.Set<ClientIdPRestriction>().Remove(client);
                    await _uow.SaveChangesAsync();
                    result.Update(true, "success", "رکورد مورد نظر با موفقیت حذف شد.");
                }
            }
            catch(Exception ex)
            {
                result.Update(false, "danger", "خطا در حذف رکورد: " + ex.Message);
            }
            return result;
        }
        #endregion

    }
}