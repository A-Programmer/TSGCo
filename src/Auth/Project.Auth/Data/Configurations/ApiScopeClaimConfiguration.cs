using Project.Auth.Domain.IdentityServer4Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.DataLayer.Configurations
{
    public class ApiScopeClaimConfiguration : IEntityTypeConfiguration<ApiScopeClaim>
    {
        public void Configure(EntityTypeBuilder<ApiScopeClaim> apiScopeClaim)
        {
            apiScopeClaim.HasKey(x => x.Id);
            apiScopeClaim.Property(x => x.Type).HasMaxLength(200).IsRequired();
        }
    }
}