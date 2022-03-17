// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


#pragma warning disable 1591

using IdentityServer4;
using KSFramework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ApiSecret : BaseEntity<Guid>
    {
        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime? Expiration { get; set; }
        public string Type { get; set; } = IdentityServerConstants.SecretTypes.SharedSecret;
        public virtual ApiResource ApiResource { get; set; }
        public Guid ApiResourceId { get; set; }

        public ApiSecret()
        {
        }
        public ApiSecret(string description, string value, string type = IdentityServerConstants.SecretTypes.SharedSecret)
        {
            Description = description;
            Value = value;
            Type = type;
        }
        public void Update(string description, string value, string type = IdentityServerConstants.SecretTypes.SharedSecret)
        {
            Description = description;
            Value = value;
            Type = type;
        }
        public void SetExpirationDate(DateTime? expirationDate)
        {
            Expiration = expirationDate;
        }
        public void SetApiResourceId(Guid apiResourceId)
        {
            ApiResourceId = apiResourceId;
        }
    }

    public class ApiSecretConfiguration : IEntityTypeConfiguration<ApiSecret>
    {
        public void Configure(EntityTypeBuilder<ApiSecret> apiSecret)
        {
            apiSecret.HasKey(x => x.Id);

            apiSecret.Property(x => x.Description).HasMaxLength(1000);
            apiSecret.Property(x => x.Value).HasMaxLength(2000);
            apiSecret.Property(x => x.Type).HasMaxLength(250);
        }
    }
}