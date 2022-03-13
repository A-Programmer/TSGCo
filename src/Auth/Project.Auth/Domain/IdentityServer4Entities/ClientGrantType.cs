// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


#pragma warning disable 1591

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ClientGrantType
    {
        public int Id { get; set; }
        public string GrantType { get; set; }
        public Client Client { get; set; }
    }

    public class ClientGrantTypeConfiguration : IEntityTypeConfiguration<ClientGrantType>
    {
        public void Configure(EntityTypeBuilder<ClientGrantType> grantType)
        {
            grantType.Property(x => x.GrantType).HasMaxLength(250).IsRequired();
        }
    }
}