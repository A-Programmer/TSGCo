// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


#pragma warning disable 1591

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ApiSecret : Secret
    {
        public ApiResource ApiResource { get; set; }
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