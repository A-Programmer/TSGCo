using Project.Auth.Domain.IdentityServer4Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.DataLayer.Configurations
{
    public class ClientPropertyConfiguration : IEntityTypeConfiguration<ClientProperty>
    {
        public void Configure(EntityTypeBuilder<ClientProperty> property)
        {
            property.Property(x => x.Key).HasMaxLength(250).IsRequired();
            property.Property(x => x.Value).HasMaxLength(2000).IsRequired();
        }
    }
}