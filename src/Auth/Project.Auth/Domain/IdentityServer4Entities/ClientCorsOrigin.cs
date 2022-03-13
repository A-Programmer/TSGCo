// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


#pragma warning disable 1591

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ClientCorsOrigin
    {
        public int Id { get; set; }
        public string Origin { get; set; }
        public Client Client { get; set; }
    }

    public class ClientCorsOriginConfiguration : IEntityTypeConfiguration<ClientCorsOrigin>
    {
        public void Configure(EntityTypeBuilder<ClientCorsOrigin> corsOrigin)
        {
            corsOrigin.Property(x => x.Origin).HasMaxLength(150).IsRequired();
        }
    }
}