using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Auth.Utilities;

namespace Project.Auth.Domain
{
    public class User
    {
        [Key]
        [MaxLength(50)]
        public string SubjectId { get; set; }

        [MaxLength(100)]
        [Required]
        public string Username { get; set; }

        [MaxLength(100)]
        public string Password { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public ICollection<UserClaim> UserClaims { get; set; } = new List<UserClaim>();

        public ICollection<UserLogin> UserLogins { get; set; } = new List<UserLogin>();

    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    IsActive = true,
                    SubjectId = "d860efca-22d9-47fd-8249-791ba61b07c7",
                    Username = "u1",
                    Password = "password".GetSha256Hash()
                },
                new User
                {
                    IsActive = true,
                    SubjectId = "b7539694-97e7-4dfe-84da-b4256e1ff5c7",
                    Username = "u2",
                    Password = "password".GetSha256Hash()
                }
            );
        }
    }
}
