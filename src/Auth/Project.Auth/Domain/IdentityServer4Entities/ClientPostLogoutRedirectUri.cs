// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


#pragma warning disable 1591

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ClientPostLogoutRedirectUri
    {
        public int Id { get; set; }
        public string PostLogoutRedirectUri { get; set; }
        public Client Client { get; set; }
    }

    public class ClientPostLogoutRedirectUriConfiguration : IEntityTypeConfiguration<ClientPostLogoutRedirectUri>
    {
        public void Configure(EntityTypeBuilder<ClientPostLogoutRedirectUri> postLogoutRedirectUri)
        {
            postLogoutRedirectUri.Property(x => x.PostLogoutRedirectUri).HasMaxLength(2000).IsRequired();
        }
    }
}