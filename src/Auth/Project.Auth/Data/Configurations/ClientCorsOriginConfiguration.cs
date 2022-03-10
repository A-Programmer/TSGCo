using Project.Auth.Domain.IdentityServer4Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.DataLayer.Configurations
{
    public class ClientCorsOriginConfiguration : IEntityTypeConfiguration<ClientCorsOrigin>
    {
        public void Configure(EntityTypeBuilder<ClientCorsOrigin> corsOrigin)
        {
            corsOrigin.Property(x => x.Origin).HasMaxLength(150).IsRequired();
        }
    }
}