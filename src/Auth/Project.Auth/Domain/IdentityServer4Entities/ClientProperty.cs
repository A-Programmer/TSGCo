// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


#pragma warning disable 1591

using KSFramework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ClientProperty : BaseEntity<Guid>
    {
        public string Key { get; private set; }
        public string Value { get; private set; }
        public virtual Client Client { get; protected set; }
        public Guid ClientId { get; private set; }

        private ClientProperty()
        {
        }
        public ClientProperty(string key, string value)
        {
            Key = key;
            Value = value;
        }
        public void SetClientId(Guid clientId)
        {
            ClientId = clientId;
        }
        public void Update(string key, string value)
        {
            Key = key;
            Value = value;
        }
    }

    public class ClientPropertyConfiguration : IEntityTypeConfiguration<ClientProperty>
    {
        public void Configure(EntityTypeBuilder<ClientProperty> property)
        {
            property.HasKey(x => x.Id);
            property.Property(x => x.Key).HasMaxLength(250).IsRequired();
            property.Property(x => x.Value).HasMaxLength(2000).IsRequired();
        }
    }
}