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

        private IdentityResource()
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
            _identityClaims.Add(identityClaim);
        }
        public void RemoveIdentityClaim(IdentityClaim identityClaim)
        {
            _identityClaims.Remove(identityClaim);
        }
        public void ClearIdentityClaims()
        {
            _identityClaims.Clear();
        }

        public void Enable() => Enabled = true;
        public void Disable() => Enabled = false;

        public void SetRequired() => Required = true;
        public void SetNotRequired() => Required = false;

        public void SetEmphasize() => Emphasize = true;
        public void NotEmphasize() => Emphasize = false;

        public void ShowResourceInDiscoveryDocument() => ShowInDiscoveryDocument = true;
        public void DontShowResourceInDiscoveryDocument() => ShowInDiscoveryDocument = false;

        public bool Enabled { get; private set; } = true;
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public string Description { get; private set; }
        public bool Required { get; private set; }
        public bool Emphasize { get; private set; }
        public bool ShowInDiscoveryDocument { get; private set; } = true;
        private List<IdentityClaim> _identityClaims = new List<IdentityClaim>();
        public IReadOnlyCollection<IdentityClaim> IdentityClaims
        {
            get { return _identityClaims.AsReadOnly(); }
        }
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
