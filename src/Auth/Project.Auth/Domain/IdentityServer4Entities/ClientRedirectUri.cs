// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


#pragma warning disable 1591

using KSFramework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ClientRedirectUri : BaseEntity<Guid>
    {
        public string RedirectUri { get; private set; }
        public virtual Client Client { get; protected set; }
        public Guid ClientId { get; private set; }

        private ClientRedirectUri()
        {
        }
        public ClientRedirectUri(string redirectUri)
        {
            RedirectUri = redirectUri;
        }
        public void SetClientId(Guid clientId)
        {
            ClientId = clientId;
        }
        public void Update(string redirectUri)
        {
            RedirectUri = redirectUri;
        }
    }

    public class ClientRedirectUriConfiguration : IEntityTypeConfiguration<ClientRedirectUri>
    {
        public void Configure(EntityTypeBuilder<ClientRedirectUri> redirectUri)
        {
            redirectUri.HasKey(x => x.Id);
            redirectUri.Property(x => x.RedirectUri).HasMaxLength(2000).IsRequired();
        }
    }
}