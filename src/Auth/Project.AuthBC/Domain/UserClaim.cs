using System.ComponentModel.DataAnnotations;
using KSFramework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain
{
    public class UserClaim : BaseEntity<Guid>
    {
        [MaxLength(50)]
        [Required]
        public Guid UserId { get; set; }
        
        public virtual User User { get; set; }

        [Required]
        [MaxLength(250)]
        public string ClaimType { get; set; }

        [Required]
        [MaxLength(250)]
        public string ClaimValue { get; set; }

        public void UpdateClaimValue(string claimValue)
        {
            ClaimValue = claimValue;
        }

        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }

        public UserClaim(string claimType, string claimValue)
        {
            ClaimType = claimType;
            ClaimValue = claimValue;
        }

        public UserClaim()
        {
        }
    }

    public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(userClaims => userClaims.User)
                .WithMany(user => user.UserClaims)
                .HasForeignKey(userClaims => userClaims.UserId);
            builder.HasIndex(userClaim => new { userClaim.UserId, userClaim.ClaimType });
        }
    }
}
