// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

#pragma warning disable 1591

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ApiScopeClaim : UserClaim
    {
        public ApiScope ApiScope { get; set; }
    }

    public class ApiScopeClaimConfiguration : IEntityTypeConfiguration<ApiScopeClaim>
    {
        public void Configure(EntityTypeBuilder<ApiScopeClaim> apiScopeClaim)
        {
            apiScopeClaim.HasKey(x => x.Id);
            apiScopeClaim.Property(x => x.Type).HasMaxLength(200).IsRequired();
        }
    }
}