// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


#pragma warning disable 1591

using KSFramework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ClientPostLogoutRedirectUri : BaseEntity<Guid>
    {
        public string PostLogoutRedirectUri { get; set; }
        public virtual Client Client { get; set; }
        public Guid ClientId { get; set; }

        public ClientPostLogoutRedirectUri()
        {
        }
        public ClientPostLogoutRedirectUri(string postLogoutRedirectUri)
        {
            PostLogoutRedirectUri = postLogoutRedirectUri;
        }
        public void SetClientId(Guid clientId)
        {
            ClientId = clientId;
        }
        public void Update(string postLogoutRedirectUri)
        {
            PostLogoutRedirectUri = postLogoutRedirectUri;
        }
    }

    public class ClientPostLogoutRedirectUriConfiguration : IEntityTypeConfiguration<ClientPostLogoutRedirectUri>
    {
        public void Configure(EntityTypeBuilder<ClientPostLogoutRedirectUri> postLogoutRedirectUri)
        {
            postLogoutRedirectUri.HasKey(x => x.Id);
            postLogoutRedirectUri.Property(x => x.PostLogoutRedirectUri).HasMaxLength(2000).IsRequired();
        }
    }
}