// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


#pragma warning disable 1591

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ClientClaim
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public Client Client { get; set; }
    }

    public class ClientClaimConfiguration : IEntityTypeConfiguration<ClientClaim>
    {
        public void Configure(EntityTypeBuilder<ClientClaim> claim)
        {
            claim.Property(x => x.Type).HasMaxLength(250).IsRequired();
            claim.Property(x => x.Value).HasMaxLength(250).IsRequired();
        }
    }
}