﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


#pragma warning disable 1591

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class PersistedGrant
    {
        public string Key { get; set; }
        public string Type { get; set; }
        public string SubjectId { get; set; }
        public string ClientId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? Expiration { get; set; }
        public string Data { get; set; }
    }

    public class PersistedGrantConfiguration : IEntityTypeConfiguration<PersistedGrant>
    {
        public void Configure(EntityTypeBuilder<PersistedGrant> grant)
        {
            grant.Property(x => x.Key).HasMaxLength(200).ValueGeneratedNever();
            grant.Property(x => x.Type).HasMaxLength(50).IsRequired();
            grant.Property(x => x.SubjectId).HasMaxLength(200);
            grant.Property(x => x.ClientId).HasMaxLength(200).IsRequired();
            grant.Property(x => x.CreationTime).IsRequired();
            // 50000 chosen to be explicit to allow enough size to avoid truncation, yet stay beneath the MySql row size limit of ~65K
            // apparently anything over 4K converts to nvarchar(max) on SqlServer
            grant.Property(x => x.Data).HasMaxLength(50000).IsRequired();

            grant.HasKey(x => x.Key);

            grant.HasIndex(x => new { x.SubjectId, x.ClientId, x.Type });
        }
    }
}