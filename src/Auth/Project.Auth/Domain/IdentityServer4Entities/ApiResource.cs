// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

#pragma warning disable 1591

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ApiResource
    {
        public int Id { get; set; }
        public bool Enabled { get; set; } = true;
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public List<ApiSecret> Secrets { get; set; }
        public List<ApiScope> Scopes { get; set; }
        public List<ApiResourceClaim> UserClaims { get; set; }
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
            apiResource.HasMany(x => x.UserClaims).WithOne(x => x.ApiResource).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
