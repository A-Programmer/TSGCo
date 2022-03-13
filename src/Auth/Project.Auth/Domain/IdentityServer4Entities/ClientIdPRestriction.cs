#pragma warning disable 1591

using KSFramework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain.IdentityServer4Entities
{
    public class ClientIdPRestriction : BaseEntity<Guid>
    {
        public string Provider { get; private set; }
        public virtual Client Client { get; protected set; }
        public Guid ClientId { get; private set; }
        private ClientIdPRestriction()
        {
        }

        public ClientIdPRestriction(string povider)
        {
            Provider = povider;
        }
        public void SetClientId(Guid clientId)
        {
            ClientId = clientId;
        }
        public void Update(string povider)
        {
            Provider = povider;
        }
    }

    public class ClientIdPRestrictionConfiguration : IEntityTypeConfiguration<ClientIdPRestriction>
    {
        public void Configure(EntityTypeBuilder<ClientIdPRestriction> idPRestriction)
        {
            idPRestriction.Property(x => x.Provider).HasMaxLength(200).IsRequired();
        }
    }
}