using System.ComponentModel.DataAnnotations;
using KSFramework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain
{
    public class UserClaim
    {         
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public Guid UserId { get; set; }
        
        public User User { get; set; }

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
            // var subjectId1 = "d860efca-22d9-47fd-8249-791ba61b07c7";
            // var subjectId2 = "b7539694-97e7-4dfe-84da-b4256e1ff5c7";
            // builder.HasData(
            //     // ---- User 1
            //     new UserClaim { Id = 1, SubjectId = subjectId1, ClaimType = "given_name", ClaimValue = "Vahid" },
            //     new UserClaim { Id = 2, SubjectId = subjectId1, ClaimType = "family_name", ClaimValue = "N" },
            //     new UserClaim { Id = 3, SubjectId = subjectId1, ClaimType = "address", ClaimValue = "Main Road 1" },
            //     new UserClaim { Id = 4, SubjectId = subjectId1, ClaimType = "role", ClaimValue = "PayingUser" },
            //     new UserClaim { Id = 5, SubjectId = subjectId1, ClaimType = "role", ClaimValue = "Test" },
            //     new UserClaim
            //     { Id = 6, SubjectId = subjectId1, ClaimType = "subscriptionlevel", ClaimValue = "PayingUser" },
            //     new UserClaim { Id = 7, SubjectId = subjectId1, ClaimType = "country", ClaimValue = "ir" },
            //     // ---- User 2
            //     new UserClaim { Id = 8, SubjectId = subjectId2, ClaimType = "given_name", ClaimValue = "User 2" },
            //     new UserClaim { Id = 9, SubjectId = subjectId2, ClaimType = "family_name", ClaimValue = "Test" },
            //     new UserClaim { Id = 10, SubjectId = subjectId2, ClaimType = "address", ClaimValue = "Big Street 2" },
            //     new UserClaim { Id = 11, SubjectId = subjectId2, ClaimType = "role", ClaimValue = "FreeUser" },
            //     new UserClaim { Id = 12, SubjectId = subjectId2, ClaimType = "role", ClaimValue = "Test" },
            //     new UserClaim
            //     { Id = 13, SubjectId = subjectId2, ClaimType = "subscriptionlevel", ClaimValue = "FreeUser" },
            //     new UserClaim { Id = 14, SubjectId = subjectId2, ClaimType = "country", ClaimValue = "be" }
            // );

            builder.HasOne(userClaim => userClaim.User)
                .WithMany(user => user.UserClaims)
                .HasForeignKey(userClaim => userClaim.UserId);

            builder.HasIndex(userClaim => new { userClaim.UserId, userClaim.ClaimType });
        }
    }
}
