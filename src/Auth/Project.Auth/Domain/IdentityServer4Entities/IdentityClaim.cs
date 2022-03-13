// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


#pragma warning disable 1591

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class IdentityClaim : UserClaim
    {
        public IdentityResource IdentityResource { get; set; }
    }

    public class IdentityClaimConfiguration : IEntityTypeConfiguration<IdentityClaim>
    {
        public void Configure(EntityTypeBuilder<IdentityClaim> claim)
        {
            claim.HasKey(x => x.Id);
            claim.Property(x => x.Type).HasMaxLength(200).IsRequired();
        }
    }
}