// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


#pragma warning disable 1591

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ClientScope
    {
        public int Id { get; set; }
        public string Scope { get; set; }
        public Client Client { get; set; }
    }

    public class ClientScopeConfiguration : IEntityTypeConfiguration<ClientScope>
    {
        public void Configure(EntityTypeBuilder<ClientScope> scope)
        {
            scope.Property(x => x.Scope).HasMaxLength(200).IsRequired();
        }
    }
}