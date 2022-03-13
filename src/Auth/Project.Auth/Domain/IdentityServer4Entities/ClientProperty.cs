// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


#pragma warning disable 1591

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ClientProperty
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public Client Client { get; set; }
    }

    public class ClientPropertyConfiguration : IEntityTypeConfiguration<ClientProperty>
    {
        public void Configure(EntityTypeBuilder<ClientProperty> property)
        {
            property.Property(x => x.Key).HasMaxLength(250).IsRequired();
            property.Property(x => x.Value).HasMaxLength(2000).IsRequired();
        }
    }
}