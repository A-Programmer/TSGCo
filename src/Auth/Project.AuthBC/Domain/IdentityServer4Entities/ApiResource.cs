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
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        
        public virtual ICollection<ApiSecret> Secrets { get; set; }
        public virtual ICollection<ApiScope> Scopes { get; set; }
        public virtual IReadOnlyCollection<ApiResourceClaim> ApiResourceClaims { get; set; }

        public ApiResource()
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
            Secrets.Add(secret);
        }
        public void RemoveSecret(ApiSecret secret)
        {
            Secrets.Remove(secret);
        }
        public void ClearSecrets()
        {
            Secrets.Clear();
        }

        public void AddScope(ApiScope scope)
        {
            Scopes.Add(scope);
        }
        public void RemoveScope(ApiScope scope)
        {
            Scopes.Remove(scope);
        }
        public void ClearScopes()
        {
            Scopes.Clear();
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
