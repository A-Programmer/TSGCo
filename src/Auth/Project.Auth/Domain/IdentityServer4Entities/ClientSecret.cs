// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


#pragma warning disable 1591

using IdentityServer4;
using IdentityServer4.Models;
using KSFramework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ClientSecret : BaseEntity<Guid>
    {
        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime? Expiration { get; set; }
        public string Type { get; set; } = IdentityServerConstants.SecretTypes.SharedSecret;
        public virtual Client Client { get; protected set; }
        public Guid ClientId { get; private set; }

        private ClientSecret()
        {
        }
        public ClientSecret(string description, string value, DateTime? expirationDate, string type = IdentityServerConstants.SecretTypes.SharedSecret)
        {
            Description = description;
            Value = value.Sha256();
            Type = type;
            Expiration = expirationDate;
        }
        public void SetClientId(Guid clientId)
        {
            ClientId = clientId;
        }
        public void Update(string description, string value, DateTime? expirationDate, string type = IdentityServerConstants.SecretTypes.SharedSecret)
        {
            Description = description;
            Value = value;
            Type = type;
            Expiration = expirationDate;
        }
    }


    public class ClientSecretConfiguration : IEntityTypeConfiguration<ClientSecret>
    {
        public void Configure(EntityTypeBuilder<ClientSecret> secret)
        {
            secret.HasKey(x => x.Id);
            secret.Property(x => x.Value).HasMaxLength(2000).IsRequired();
            secret.Property(x => x.Type).HasMaxLength(250);
            secret.Property(x => x.Description).HasMaxLength(2000);
        }
    }
}