using KSFramework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Auth.Domain
{
    public class UserLogin : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        
        public User User { get; set; }

        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }


        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }

        public UserLogin(string loginProvider, string providerKey)
        {
            LoginProvider = loginProvider;
            ProviderKey = providerKey;
        }
        private UserLogin()
        {
            
        }
    }

    public class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(userLogin => userLogin.User)
                .WithMany(user => user.UserLogins)
                .HasForeignKey(userLogin => userLogin.UserId);
            builder.HasIndex(userLogin => new {userLogin.UserId, userLogin.LoginProvider}).IsUnique();
        }
    }
}
