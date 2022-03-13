// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


#pragma warning disable 1591

using KSFramework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ClientGrantType : BaseEntity<Guid>
    {
        public string GrantType { get; private set; }
        public virtual Client Client { get; protected set; }
        public Guid ClientId { get; private set; }

        private ClientGrantType()
        {
        }
        public ClientGrantType(string grantType)
        {
            GrantType = grantType;
        }
        public void SetClientId(Guid clientId)
        {
            ClientId = clientId;
        }
        public void Update(string grantType)
        {
            GrantType = grantType;
        }
    }

    public class ClientGrantTypeConfiguration : IEntityTypeConfiguration<ClientGrantType>
    {
        public void Configure(EntityTypeBuilder<ClientGrantType> grantType)
        {
            grantType.HasKey(x => x.Id);
            grantType.Property(x => x.GrantType).HasMaxLength(250).IsRequired();
        }
    }
}