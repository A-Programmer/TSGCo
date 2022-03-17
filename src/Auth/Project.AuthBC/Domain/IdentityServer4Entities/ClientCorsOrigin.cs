// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


#pragma warning disable 1591

using KSFramework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ClientCorsOrigin : BaseEntity<Guid>
    {
        public string Origin { get; set; }
        public virtual Client Client { get; set; }
        public Guid ClientId { get; set; }

        public ClientCorsOrigin()
        {
        }
        public ClientCorsOrigin(string origin)
        {
            Origin = origin;
        }
        public void SetClientId(Guid clientId)
        {
            ClientId = clientId;
        }
        public void Update(string origin)
        {
            Origin = origin;
        }
    }

    public class ClientCorsOriginConfiguration : IEntityTypeConfiguration<ClientCorsOrigin>
    {
        public void Configure(EntityTypeBuilder<ClientCorsOrigin> corsOrigin)
        {
            corsOrigin.HasKey(x => x.Id);
            corsOrigin.Property(x => x.Origin).HasMaxLength(150).IsRequired();
        }
    }
}