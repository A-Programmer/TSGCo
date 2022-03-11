using KSFramework.Domain;
using KSFramework.Domain.AggregatesHelper;

namespace Project.Auth.Domain
{
    public class User : BaseEntity<Guid>, IAggregateRoot
    {
        public string UserName { get; private set; }

        public string HashedPassword { get; private set; }

        public bool IsActive { get; private set; }

        public virtual IReadOnlyCollection<UserClaim> UserClaims => _userClaims;
        protected List<UserClaim> _userClaims = new List<UserClaim>();

        public virtual IReadOnlyCollection<UserLogin> UserLogins => _userLogins;
        protected List<UserLogin> _userLogins = new List<UserLogin>();



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
}