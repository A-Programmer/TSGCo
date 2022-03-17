
using Microsoft.EntityFrameworkCore;
using Project.Auth.Contracts;
using Project.Auth.Domain;
using Project.Auth.Domain.IdentityServer4Entities;
using Project.Auth.Utilities;

namespace Project.Auth.Data
{
    public class DataSeeder
    {
        private readonly IUnitOfWork _uow;
        private readonly DbSet<User> Users;
        private readonly DbSet<Client> Clients;
        private readonly DbSet<IdentityResource> IdentityResources;
        public DataSeeder(IUnitOfWork uow)
        {
            _uow = uow;
            Users = _uow.Set<User>();
            Clients = _uow.Set<Client>();
            IdentityResources = _uow.Set<IdentityResource>();
        }

        public void Seed()
        {
            #region Seed Users
            var adminUserId = Guid.NewGuid();
            var admin = new User("admin", "admin".GetSha256Hash(), true) { Id = adminUserId };
            #region UserClaims
            var adminFirstName = new UserClaim("given_name", "Kamran");
            adminFirstName.SetUserId(adminUserId);
            var adminLastName = new UserClaim("family_name", "Sadin");
            adminLastName.SetUserId(adminUserId);
            var adminRole = new UserClaim("role", "admin");
            var testRole = new UserClaim("role", "test");
            adminRole.SetUserId(adminUserId);
            testRole.SetUserId(adminUserId);
            #endregion

            var userId = Guid.NewGuid();
            var user = new User("user", "user".GetSha256Hash(), true) { Id = userId };
            #region UserClaims
            var userFName = new UserClaim("given_name", "Mohsen");
            userFName.SetUserId(userId);
            var userLName = new UserClaim("family_name", "Safari");
            userLName.SetUserId(userId);
            var userRole = new UserClaim("role", "user");
            userRole.SetUserId(userId);

            if(!Users.Any())
            {
                Users.Add(admin);
                Users.Add(user);
            }
            #endregion
            #endregion

            //Seed Clients
            var clientId = Guid.NewGuid();
            var client = new Client("cmsclient", "CMS Client", "CMS Client description", "https://localhost:7001", "https://localhost:7001/assets/images/logo.png", "https://localhost:7001/back-logout", "https://localhost:7001/front-logour",
                false, true, true, true, true, true, true, true, true, true, true, true, false, true, true)
            {
                Id = clientId
            };
            //Client Grant Types
            var clientGrantType = new ClientGrantType("hybrid") { Id = Guid.NewGuid() };
            clientGrantType.SetClientId(clientId);
            client.AddClientGrant(clientGrantType);

            //Client Redirect Uris
            var clientRedirectUri = new ClientRedirectUri("https://localhost:7001/signin-oidc") { Id = Guid.NewGuid() };
            clientRedirectUri.SetClientId(clientId);
            client.AddClientRedirectUri(clientRedirectUri);

            //Client Scopes
            var offlineAccessClientScopes = new ClientScope("offline_access") { Id = Guid.NewGuid() };
            offlineAccessClientScopes.SetClientId(clientId);
            client.AddClientScope(offlineAccessClientScopes);
            var openidClientScopes = new ClientScope("openid") { Id = Guid.NewGuid() };
            openidClientScopes.SetClientId(clientId);
            client.AddClientScope(openidClientScopes);
            var profileClientScopes = new ClientScope("profile") { Id = Guid.NewGuid() };
            profileClientScopes.SetClientId(clientId);
            client.AddClientScope(profileClientScopes);
            var addressClientScopes = new ClientScope("address") { Id = Guid.NewGuid() };
            addressClientScopes.SetClientId(clientId);
            client.AddClientScope(addressClientScopes);
            var roleClientScopes = new ClientScope("role") { Id = Guid.NewGuid() };
            roleClientScopes.SetClientId(clientId);
            client.AddClientScope(roleClientScopes);

            //Client Secrets
            var clientSecret = new ClientSecret("CMS Client Secret", "CMSClientSecretKeyGoesHere", DateTime.Now.AddDays(20));
            client.AddClientSecret(clientSecret);

            //Identity Resource
            var roleIdentityResource = new IdentityResource("opeinid", "Open Id", "Open Id Resource", false, false, true, true);
            //Identity Claims
            var roleIdentityResourceClaim = new IdentityClaim("sub");
            roleIdentityResourceClaim.SetIdentityResourceId(roleIdentityResource.Id);

            if(!Clients.Any())
            {
                Clients.Add(client);
            }

            if(!IdentityResources.Any())
            {
                IdentityResources.Add(roleIdentityResource);
            }

            _uow.SaveChanges();
            
        }
    }
}