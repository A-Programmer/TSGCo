// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


#pragma warning disable 1591

using KSFramework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ClientScope : BaseEntity<Guid>
    {
        public string Scope { get; set; }
        public virtual Client Client { get; set; }
        public Guid ClientId { get; set; }

        public ClientScope()
        {
        }
        public ClientScope(string scope)
        {
            Scope = scope;
        }
        public void SetClientId(Guid clientId)
        {
            ClientId = clientId;
        }
        public void Update(string scope)
        {
            Scope = scope;
        }
    }

    public class ClientScopeConfiguration : IEntityTypeConfiguration<ClientScope>
    {
        public void Configure(EntityTypeBuilder<ClientScope> scope)
        {
            scope.HasKey(x => x.Id);
            scope.Property(x => x.Scope).HasMaxLength(200).IsRequired();
        }
    }
}