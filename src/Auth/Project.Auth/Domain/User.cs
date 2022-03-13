using System.ComponentModel.DataAnnotations.Schema;
using KSFramework.Domain;
using KSFramework.Domain.AggregatesHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Auth.Utilities;

namespace Project.Auth.Domain
{
    public class User : BaseEntity<Guid>, IAggregateRoot
    {
        public string UserName { get; private set; }

        public string HashedPassword { get; private set; }

        public bool IsActive { get; private set; }

        private List<UserClaim> _userClaims = new List<UserClaim>();
        [NotMapped]
        public IReadOnlyCollection<UserClaim> UserClaims
        {
            get { return _userClaims.AsReadOnly(); }
        }

        private List<UserLogin> _userLogins = new List<UserLogin>();
        [NotMapped]
        public IReadOnlyCollection<UserLogin> UserLogins
        {
            get { return _userLogins.AsReadOnly(); }
        }



        private User()
        {

        }

        public User(string userName, string hashedPassword, bool isActive = false)
        {
            if(string.IsNullOrEmpty(userName))
                throw new ArgumentNullException($"{nameof(UserName)} can not be null");
            
            UserName = userName;

            HashedPassword = hashedPassword;

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
            _userClaims.Add(claim);
        }
        public void RemoveClaim(UserClaim claim)
        {
            _userClaims.Remove(claim);
        }
        public void ClearClaims()
        {
            _userClaims.Clear();
        }

        public void AddLogin(UserLogin login)
        {
            _userLogins.Add(login);
        }
        public void RemoveLogin(UserLogin login)
        {
            _userLogins.Remove(login);
        }
        public void ClearLogins()
        {
            _userLogins.Clear();
        }

    }

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            
            var admin = new User("admin", "admin".GetSha256Hash(), true) { Id = Guid.NewGuid() };
            #region UserClaims
            var adminFirstName = new UserClaim("given_name", "Kamran");
            adminFirstName.SetUserId(admin.Id);
            var adminLastName = new UserClaim("family_name", "Sadin");
            adminLastName.SetUserId(admin.Id);
            var adminRole = new UserClaim("role", "admin");
            adminRole.SetUserId(admin.Id);
            #endregion


            var user = new User("user", "user".GetSha256Hash(), true) { Id = Guid.NewGuid() };
            #region UserClaims
            var userFName = new UserClaim("given_name", "Mohsen");
            userFName.SetUserId(admin.Id);
            var userLName = new UserClaim("family_name", "Safari");
            userLName.SetUserId(admin.Id);
            var userRole = new UserClaim("role", "user");
            userRole.SetUserId(admin.Id);
            #endregion

            builder.HasData(
                admin,
                user
            );
        }
    }
}