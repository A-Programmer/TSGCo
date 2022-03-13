// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

#pragma warning disable 1591

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ApiScope
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument { get; set; } = true;
        public List<ApiScopeClaim> UserClaims { get; set; }

        public ApiResource ApiResource { get; set; }
    }

    public class ApiScopeConfiguration : IEntityTypeConfiguration<ApiScope>
    {
        public void Configure(EntityTypeBuilder<ApiScope> apiScope)
        {
            apiScope.HasKey(x => x.Id);

            apiScope.Property(x => x.Name).HasMaxLength(200).IsRequired();
            apiScope.Property(x => x.DisplayName).HasMaxLength(200);
            apiScope.Property(x => x.Description).HasMaxLength(1000);

            apiScope.HasIndex(x => x.Name).IsUnique();

            apiScope.HasMany(x => x.UserClaims).WithOne(x => x.ApiScope).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}