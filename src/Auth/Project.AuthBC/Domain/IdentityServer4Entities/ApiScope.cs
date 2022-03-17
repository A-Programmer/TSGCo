// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

#pragma warning disable 1591

using System.Collections.Generic;
using KSFramework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ApiScope : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument { get; set; } = true;
        
        public virtual ICollection<ApiScopeClaim> ApiScopeClaims { get; set; }

        public virtual ApiResource ApiResource { get; set; }
        public Guid ApiResourceId { get; set; }

        public ApiScope()
        {
        }

        public ApiScope(string name, string displayName, string description, bool isRequired, bool emphasize, bool showInDiscoveryDocument = true)
        {
            Name = name;
            DisplayName = displayName;
            Description = description;
            Required = isRequired;
            Emphasize = emphasize;
            ShowInDiscoveryDocument = showInDiscoveryDocument;
        }

        public void Update(string name, string displayName, string description, bool isRequired, bool emphasize)
        {
            Name = name;
            DisplayName = displayName;
            Description = description;
            Required = isRequired;
            Emphasize = emphasize;
        }

        public void SetRequired()
        {
            Required = true;
        }
        public void SetNotRequired()
        {
            Required = false;
        }
        public void EmphasizeItem()
        {
            Emphasize = false;
        }
        public void NotEmphasize()
        {
            Emphasize = false;
        }
        public void ShowItInDiscoveryDocument()
        {
            ShowInDiscoveryDocument = true;
        }
        public void DontShowInDiscoveryDocument()
        {
            ShowInDiscoveryDocument = false;
        }
    }

    public class ApiScopeConfiguration : IEntityTypeConfiguration<ApiScope>
    {
        public void Configure(EntityTypeBuilder<ApiScope> apiScope)
        {
            apiScope.HasKey(x => x.Id);

            apiScope.Property(x => x.Name).HasMaxLength(200).IsRequired();
            apiScope.Property(x => x.DisplayName).HasMaxLength(200);
            apiScope.Property(x => x.Description).HasMaxLength(1000);

            apiScope.HasIndex(x => x.Name).IsUnique();

            apiScope.HasMany(x => x.ApiScopeClaims).WithOne(x => x.ApiScope).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}