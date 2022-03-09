using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain
{
    public class UserClaim
    {
        public UserClaim()
        {

        }

        public UserClaim(string userId, string claimType, string claimValue)
        {
            SubjectId = userId;
            ClaimType = claimType;
            ClaimValue = claimValue;
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string SubjectId { get; set; }

        public User User { get; set; }

        [Required]
        [MaxLength(250)]
        public string ClaimType { get; set; }

        [Required]
        [MaxLength(250)]
        public string ClaimValue { get; set; }
    }

    public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            var subjectId1 = "d860efca-22d9-47fd-8249-791ba61b07c7";
            var subjectId2 = "b7539694-97e7-4dfe-84da-b4256e1ff5c7";
            builder.HasData(
                //User1
                new UserClaim { Id = 1, SubjectId = subjectId1, ClaimType = "given_name", ClaimValue = "Kamran" },
                new UserClaim { Id = 2, SubjectId = subjectId1, ClaimType = "family_name", ClaimValue = "Sadin" },
                new UserClaim { Id = 3, SubjectId = subjectId1, ClaimType = "address", ClaimValue = "Iran, Golestan, Gonbade Kavous" },
                new UserClaim { Id = 4, SubjectId = subjectId1, ClaimType = "role", ClaimValue = "admin" },
                new UserClaim { Id = 5, SubjectId = subjectId1, ClaimType = "role", ClaimValue = "manager" },
                new UserClaim
                { Id = 6, SubjectId = subjectId1, ClaimType = "subscriptionlevel", ClaimValue = "Gold" },
                new UserClaim { Id = 7, SubjectId = subjectId1, ClaimType = "country", ClaimValue = "iran" },


                //User2
                new UserClaim { Id = 8, SubjectId = subjectId2, ClaimType = "given_name", ClaimValue = "John" },
                new UserClaim { Id = 9, SubjectId = subjectId2, ClaimType = "family_name", ClaimValue = "Doe" },
                new UserClaim { Id = 10, SubjectId = subjectId2, ClaimType = "address", ClaimValue = "Iran, Tehran" },
                new UserClaim { Id = 11, SubjectId = subjectId2, ClaimType = "role", ClaimValue = "user" },
                new UserClaim { Id = 12, SubjectId = subjectId2, ClaimType = "role", ClaimValue = "manager" },
                new UserClaim
                { Id = 13, SubjectId = subjectId2, ClaimType = "subscriptionlevel", ClaimValue = "Silver" },
                new UserClaim { Id = 14, SubjectId = subjectId2, ClaimType = "country", ClaimValue = "USA" }
            );

            builder.HasOne(userClaim => userClaim.User)
                .WithMany(user => user.UserClaims)
                .HasForeignKey(userClaim => userClaim.SubjectId);

            builder.HasIndex(userClaim => new { userClaim.SubjectId, userClaim.ClaimType });
        }
    }

}
