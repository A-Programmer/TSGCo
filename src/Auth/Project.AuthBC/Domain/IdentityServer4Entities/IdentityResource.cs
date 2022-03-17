// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


#pragma warning disable 1591

using System.Collections.Generic;
using KSFramework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class IdentityResource : BaseEntity<Guid>
    {

        public IdentityResource()
        {
        }

        public IdentityResource(string name, string displayName, string description,
            bool isRequired, bool emphasize, bool showInDiscoveryDocument = true, bool enabled = true)
        {
            Name = name;
            DisplayName = displayName;
            Description = description;
            Required = isRequired;
            Emphasize = emphasize;
            ShowInDiscoveryDocument = showInDiscoveryDocument;
            Enabled = enabled;
        }
        public void Update(string name, string displayName, string description)
        {
            Name = name;
            DisplayName = displayName;
            Description = description;
        }

        public void AddIdentityClaim(IdentityClaim identityClaim)
        {
            IdentityClaims.Add(identityClaim);
        }
        public void RemoveIdentityClaim(IdentityClaim identityClaim)
        {
            IdentityClaims.Remove(identityClaim);
        }
        public void ClearIdentityClaims()
        {
            IdentityClaims.Clear();
        }

        public void Enable() => Enabled = true;
        public void Disable() => Enabled = false;

        public void SetRequired() => Required = true;
        public void SetNotRequired() => Required = false;

        public void SetEmphasize() => Emphasize = true;
        public void NotEmphasize() => Emphasize = false;

        public void ShowResourceInDiscoveryDocument() => ShowInDiscoveryDocument = true;
        public void DontShowResourceInDiscoveryDocument() => ShowInDiscoveryDocument = false;

        public bool Enabled { get; set; } = true;
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument { get; set; } = true;
        public virtual ICollection<IdentityClaim> IdentityClaims { get; set; }
    }


    public class IdentityResourceConfiguration : IEntityTypeConfiguration<IdentityResource>
    {
        public void Configure(EntityTypeBuilder<IdentityResource> identityResource)
        {
            identityResource.HasKey(x => x.Id);

            identityResource.Property(x => x.Name).HasMaxLength(200).IsRequired();
            identityResource.Property(x => x.DisplayName).HasMaxLength(200);
            identityResource.Property(x => x.Description).HasMaxLength(1000);

            identityResource.HasIndex(x => x.Name).IsUnique();

            identityResource.HasMany(x => x.IdentityClaims)
                .WithOne(x => x.IdentityResource)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
