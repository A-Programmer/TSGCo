// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

#pragma warning disable 1591

using KSFramework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ApiResourceClaim : BaseEntity<Guid>
    {
        public string Type { get; private set; }
        public virtual ApiResource ApiResource { get; protected set; }
        public Guid ApiResourceId { get; private set; }

        private ApiResourceClaim()
        {
        }
        public ApiResourceClaim(string type)
        {
            Type = type;
        }
        public void SetApiResourceId(Guid apiResourceId)
        {
            ApiResourceId = apiResourceId;
        }
    }

    public class ApiResourceClaimConfiguration : IEntityTypeConfiguration<ApiResourceClaim>
    {
        public void Configure(EntityTypeBuilder<ApiResourceClaim> apiClaim)
        {
            apiClaim.HasKey(x => x.Id);

            apiClaim.Property(x => x.Type).HasMaxLength(200).IsRequired();
        }
    }
}