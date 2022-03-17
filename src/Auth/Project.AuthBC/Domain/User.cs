using System.ComponentModel.DataAnnotations.Schema;
using KSFramework.Domain;
using KSFramework.Domain.AggregatesHelper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Auth.Utilities;

namespace Project.Auth.Domain
{
    public class User : IdentityUser<Guid>
    {
        public bool IsActive { get; set; }

        public virtual ICollection<UserClaim> UserClaims { get; set; }
        public virtual ICollection<UserLogin> UserLogins { get; set; }


        public User()
        {

        }

        public User(string userName)
        {
            UserName = userName;
        }

        public User(string userName, string hashedPassword, bool isActive = false)
        {
            if(string.IsNullOrEmpty(userName))
                throw new ArgumentNullException($"{nameof(UserName)} can not be null");
            
            UserName = userName;

            PasswordHash = hashedPassword;

            IsActive = isActive;
        }

        public void UpdateUserName(string userName)
        {
            if(string.IsNullOrEmpty(userName))
                throw new ArgumentNullException($"{nameof(UserName)} can not be null");
            
            UserName = userName;

        }

        public void Active()
        {
            IsActive = true;
        }

        public void Deactive()
        {
            IsActive = false;
        }

        public void AddClaim(UserClaim claim)
        {
            UserClaims.Add(claim);
        }
        public void RemoveClaim(UserClaim claim)
        {
            UserClaims.Remove(claim);
        }
        public void ClearClaims()
        {
            UserClaims.Clear();
        }

        public void AddLogin(UserLogin login)
        {
            UserLogins.Add(login);
        }
        public void RemoveLogin(UserLogin login)
        {
            UserLogins.Remove(login);
        }
        public void ClearLogins()
        {
            UserLogins.Clear();
        }

    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Users");
        }
    }
}