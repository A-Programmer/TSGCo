// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


#pragma warning disable 1591

using KSFramework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ClientClaim : BaseEntity<Guid>
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public virtual Client Client { get; set; }
        public Guid ClientId { get; set; }

        public ClientClaim()
        {
        }

        public ClientClaim(string type, string value)
        {
            Type = type;
            Value = value;
        }
        public void SetClientId(Guid clientId)
        {
            ClientId = clientId;
        }
        public void Update(string type, string value)
        {
            Type = type;
            Value = value;
        }

    }

    public class ClientClaimConfiguration : IEntityTypeConfiguration<ClientClaim>
    {
        public void Configure(EntityTypeBuilder<ClientClaim> claim)
        {
            claim.HasKey(x => x.Id);
            claim.Property(x => x.Type).HasMaxLength(250).IsRequired();
            claim.Property(x => x.Value).HasMaxLength(250).IsRequired();
        }
    }
}