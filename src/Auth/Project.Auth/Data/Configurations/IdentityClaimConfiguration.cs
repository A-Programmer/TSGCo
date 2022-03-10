using Project.Auth.Domain.IdentityServer4Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.DataLayer.Configurations
{
    public class IdentityClaimConfiguration : IEntityTypeConfiguration<IdentityClaim>
    {
        public void Configure(EntityTypeBuilder<IdentityClaim> claim)
        {
            claim.HasKey(x => x.Id);
            claim.Property(x => x.Type).HasMaxLength(200).IsRequired();
        }
    }
}