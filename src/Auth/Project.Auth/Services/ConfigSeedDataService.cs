using System.Collections.Generic;
using Project.Auth.Mappings;
using System.Linq;
using Project.Auth.Contracts;
using Project.Auth.Domain.IdentityServer4Entities;

namespace Project.Auth.Services
{
    public interface IConfigSeedDataService
    {
        void EnsureSeedDataForContext(
            IEnumerable<IdentityServer4.Models.Client> clients,
            IEnumerable<IdentityServer4.Models.ApiResource> apiResources,
            IEnumerable<IdentityServer4.Models.IdentityResource> identityResources);
    }

    public class ConfigSeedDataService : IConfigSeedDataService
    {
        private readonly IUnitOfWork _uow;

        public ConfigSeedDataService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public void EnsureSeedDataForContext(
            IEnumerable<IdentityServer4.Models.Client> clients,
            IEnumerable<IdentityServer4.Models.ApiResource> apiResources,
            IEnumerable<IdentityServer4.Models.IdentityResource> identityResources)
        {
            if (!_uow.Set<Client>().Any())
            {
                foreach (var client in clients)
                {
                    _uow.Set<Client>().Add(client.ToEntity());
                }

                _uow.SaveChanges();
            }

            if (!_uow.Set<IdentityResource>().Any())
            {
                foreach (var resource in identityResources)
                {
                    _uow.Set<IdentityResource>().Add(resource.ToEntity());
                }

                _uow.SaveChanges();
            }

            if (!_uow.Set<ApiResource>().Any())
            {
                foreach (var resource in apiResources)
                {
                    _uow.Set<ApiResource>().Add(resource.ToEntity());
                }

                _uow.SaveChanges();
            }
        }
    }
}