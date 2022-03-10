using Project.Auth.Domain.IdentityServer4Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.DataLayer.Configurations
{
    public class ApiResourceClaimConfiguration : IEntityTypeConfiguration<ApiResourceClaim>
    {
        public void Configure(EntityTypeBuilder<ApiResourceClaim> apiClaim)
        {
            apiClaim.HasKey(x => x.Id);

            apiClaim.Property(x => x.Type).HasMaxLength(200).IsRequired();
        }
    }
}