// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


#pragma warning disable 1591

using KSFramework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class IdentityClaim : BaseEntity<Guid>
    {
        public string Type { get; set; }
        public virtual IdentityResource IdentityResource { get; set; }
        public Guid IdentityResourceId { get; set; }

        public IdentityClaim()
        {
        }
        public IdentityClaim(string type)
        {
            Type = type;
        }
        public void SetIdentityResourceId(Guid identityResourceId)
        {
            IdentityResourceId = identityResourceId;
        }
        public void Update(string type)
        {
            Type = type;
        }
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