// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


#pragma warning disable 1591

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ClientSecret : Secret
    {
        public Client Client { get; set; }
    }


    public class ClientSecretConfiguration : IEntityTypeConfiguration<ClientSecret>
    {
        public void Configure(EntityTypeBuilder<ClientSecret> secret)
        {
            secret.Property(x => x.Value).HasMaxLength(2000).IsRequired();
            secret.Property(x => x.Type).HasMaxLength(250);
            secret.Property(x => x.Description).HasMaxLength(2000);
        }
    }
}