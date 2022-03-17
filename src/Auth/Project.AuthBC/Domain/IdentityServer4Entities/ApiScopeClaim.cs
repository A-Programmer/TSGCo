// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

#pragma warning disable 1591

using KSFramework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ApiScopeClaim : BaseEntity<Guid>
    {
        public string Type { get; set; }
        public virtual ApiScope ApiScope { get; set; }
        public Guid ApiScopeId { get; set; }

        public ApiScopeClaim()
        {
        }

        public ApiScopeClaim(string type)
        {
            Type = type;
        }

        public void Update(string type)
        {
            Type = type;
        }

        public void SetApiScopeId(Guid apiScopeId)
        {
            ApiScopeId = apiScopeId;
        }
    }

    public class ApiScopeClaimConfiguration : IEntityTypeConfiguration<ApiScopeClaim>
    {
        public void Configure(EntityTypeBuilder<ApiScopeClaim> apiScopeClaim)
        {
            apiScopeClaim.HasKey(x => x.Id);
            apiScopeClaim.Property(x => x.Type).HasMaxLength(200).IsRequired();
        }
    }
}