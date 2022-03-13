// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

#pragma warning disable 1591

using System.Collections.Generic;
using KSFramework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ApiResource : BaseEntity<Guid>
    {
        public bool Enabled { get; private set; }
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public string Description { get; private set; }
        private List<ApiSecret> _secrets = new List<ApiSecret>();
        public IReadOnlyCollection<ApiSecret> Secrets
        {
            get { return _secrets.AsReadOnly(); }
        }
        private List<ApiScope> _scopes = new List<ApiScope>();
        public IReadOnlyCollection<ApiScope> Scopes
        {
            get { return _scopes.AsReadOnly(); }
        }

        private List<ApiResourceClaim> _apiResourceClaims = new List<ApiResourceClaim>();
        public IReadOnlyCollection<ApiResourceClaim> ApiResourceClaims
        {
            get { return _apiResourceClaims.AsReadOnly(); }
        }

        private ApiResource()
        {

        }

        public ApiResource(string name, string displayName, string description, bool enabled = true)
        {
            Name = name;
            DisplayName = displayName;
            Description = description;
            Enabled = enabled;
        }

        public void UpdateApiResource(string name, string displayName, string description)
        {
            Name = name;
            DisplayName = displayName;
            Description = description;
        }

        public void EnableResource()
        {
            Enabled = true;
        }
        public void DisableResource()
        {
            Enabled = false;
        }

        public void AddSecret(ApiSecret secret)
        {
            _secrets.Add(secret);
        }
        public void RemoveSecret(ApiSecret secret)
        {
            _secrets.Remove(secret);
        }
        public void ClearSecrets()
        {
            _secrets.Clear();
        }

        public void AddScope(ApiScope scope)
        {
            _scopes.Add(scope);
        }
        public void RemoveScope(ApiScope scope)
        {
            _scopes.Remove(scope);
        }
        public void ClearScopes()
        {
            _scopes.Clear();
        }
    }

    public class ApiResourceConfiguration : IEntityTypeConfiguration<ApiResource>
    {
        public void Configure(EntityTypeBuilder<ApiResource> apiResource)
        {
            apiResource.HasKey(x => x.Id);

            apiResource.Property(x => x.Name).HasMaxLength(200).IsRequired();
            apiResource.Property(x => x.DisplayName).HasMaxLength(200);
            apiResource.Property(x => x.Description).HasMaxLength(1000);

            apiResource.HasIndex(x => x.Name).IsUnique();

            apiResource.HasMany(x => x.Secrets).WithOne(x => x.ApiResource).IsRequired().OnDelete(DeleteBehavior.Cascade);
            apiResource.HasMany(x => x.Scopes).WithOne(x => x.ApiResource).IsRequired().OnDelete(DeleteBehavior.Cascade);
            apiResource.HasMany(x => x.ApiResourceClaims).WithOne(x => x.ApiResource).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
