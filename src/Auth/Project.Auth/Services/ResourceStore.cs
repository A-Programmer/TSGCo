// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Auth.Mappings;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project.Auth.Contracts;
using Project.Auth.Domain.IdentityServer4Entities;

namespace Project.Auth.Services
{
    /// <summary>
    /// Implementation of IResourceStore thats uses EF.
    /// </summary>
    /// <seealso cref="IdentityServer4.Stores.IResourceStore" />
    public class ResourceStore : IResourceStore
    {
        private readonly IUnitOfWork _uow;
        private readonly DbSet<ApiResource> _apiResources;
        private readonly DbSet<ApiScope> _apiScopes;
        private readonly DbSet<IdentityResource> _identityResources;
        private readonly ILogger<ResourceStore> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceStore"/> class.
        /// </summary>
        public ResourceStore(IUnitOfWork uow, ILogger<ResourceStore> logger)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _apiResources = _uow.Set<ApiResource>();
            _identityResources = _uow.Set<IdentityResource>();
            _apiScopes = _uow.Set<ApiScope>();
            _logger = logger;
        }

        /// <summary>
        /// Finds the API resource by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Task<IdentityServer4.Models.ApiResource> FindApiResourceAsync(string name)
        {
            var query =
                from apiResource in _apiResources
                where apiResource.Name == name
                select apiResource;

            var apis = query
                .Include(x => x.Secrets)
                .Include(x => x.Scopes)
                    .ThenInclude(s => s.UserClaims)
                .Include(x => x.UserClaims);

            var api = apis.FirstOrDefault();

            _logger.LogDebug(
                api != null ? "Found {api} API resource in database" : "Did not find {api} API resource in database",
                name);

            return Task.FromResult(api.ToModel());
        }

        public Task<IEnumerable<IdentityServer4.Models.ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            var names = apiResourceNames.ToArray();
            var result = _apiResources.Where(x => MemoryExtensions.Contains<string>(names, x.Name)).ToArray();
            var model = result.Select(x => x.ToModel());
            return Task.FromResult(model);
        }

        /// <summary>
        /// Gets API resources by scope name.
        /// </summary>
        /// <param name="scopeNames"></param>
        /// <returns></returns>
        public Task<IEnumerable<IdentityServer4.Models.ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var names = scopeNames.ToArray();

            var query =
                from api in _apiResources
                where api.Scopes.Any(x => names.Contains(x.Name))
                select api;

            var apis = query
                .Include(x => x.Secrets)
                .Include(x => x.Scopes)
                    .ThenInclude(s => s.UserClaims)
                .Include(x => x.UserClaims);

            var results = apis.ToArray();
            var models = results.Select(x => x.ToModel()).ToArray();

            _logger.LogDebug("Found {scopes} API scopes in database", models.SelectMany(x => x.Scopes));

            return Task.FromResult(models.AsEnumerable());
        }

        public Task<IEnumerable<IdentityServer4.Models.ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            var names = scopeNames.ToArray();
            var result = _apiScopes.Where(x => MemoryExtensions.Contains<string>(names, x.Name)).ToArray();
            var model = result.Select(x => x.ToModel());
            return Task.FromResult(model);
        }

        /// <summary>
        /// Gets identity resources by scope name.
        /// </summary>
        /// <param name="scopeNames"></param>
        /// <returns></returns>
        public Task<IEnumerable<IdentityServer4.Models.IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var scopes = scopeNames.ToArray();

            var query =
                from identityResource in _identityResources
                where scopes.Contains(identityResource.Name)
                select identityResource;

            var resources = query
                .Include(x => x.UserClaims);

            var results = resources.ToArray();

            _logger.LogDebug("Found {scopes} identity scopes in database", results.Select(x => x.Name));

            return Task.FromResult(results.Select(x => x.ToModel()).ToArray().AsEnumerable());
        }

        public Task<IEnumerable<IdentityServer4.Models.IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var scopes = scopeNames.ToArray();

            var query =
                from identityResource in _identityResources
                where scopes.Contains(identityResource.Name)
                select identityResource;

            var resources = query
                .Include(x => x.UserClaims);

            var results = resources.ToArray();

            _logger.LogDebug("Found {scopes} identity scopes in database", results.Select(x => x.Name));

            return Task.FromResult(results.Select(x => x.ToModel()).ToArray().AsEnumerable());
        }

        /// <summary>
        /// Gets all resources.
        /// </summary>
        /// <returns></returns>
        public Task<IdentityServer4.Models.Resources> GetAllResourcesAsync()
        {
            var identity = _identityResources
              .Include(x => x.UserClaims);

            var apis = _apiResources
                .Include(x => x.Secrets)
                .Include(x => x.Scopes)
                    .ThenInclude(s => s.UserClaims)
                .Include(x => x.UserClaims);
            
            var apiScopes = _apiScopes
                .Include(x => x.UserClaims);

            var result = new IdentityServer4.Models.Resources(
                identity.ToArray().Select(x => x.ToModel()).AsEnumerable(),
                apis.ToArray().Select(x => x.ToModel()).AsEnumerable(),
                apiScopes.ToArray().Select(x => x.ToModel())
                );

            // var result = new IdentityServer4.Models.Resources(
            //     identity.ToArray().Select(x => x.ToModel()).AsEnumerable(),
            //     apis.ToArray().Select(x => x.ToModel()).AsEnumerable());

            _logger.LogDebug("Found {scopes} as all scopes in database", result.IdentityResources.Select(x=>x.Name).Union(result.ApiResources.SelectMany(x=>x.Scopes)));

            return Task.FromResult(result);
        }
    }
}