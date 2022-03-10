using Project.Auth.Domain.IdentityServer4Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.DataLayer.Configurations
{
    public class ClientPostLogoutRedirectUriConfiguration : IEntityTypeConfiguration<ClientPostLogoutRedirectUri>
    {
        public void Configure(EntityTypeBuilder<ClientPostLogoutRedirectUri> postLogoutRedirectUri)
        {
            postLogoutRedirectUri.Property(x => x.PostLogoutRedirectUri).HasMaxLength(2000).IsRequired();
        }
    }
}