#pragma warning disable 1591

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ClientIdPRestriction
    {
        public int Id { get; set; }
        public string Provider { get; set; }
        public Client Client { get; set; }
    }

    public class ClientIdPRestrictionConfiguration : IEntityTypeConfiguration<ClientIdPRestriction>
    {
        public void Configure(EntityTypeBuilder<ClientIdPRestriction> idPRestriction)
        {
            idPRestriction.Property(x => x.Provider).HasMaxLength(200).IsRequired();
        }
    }
}