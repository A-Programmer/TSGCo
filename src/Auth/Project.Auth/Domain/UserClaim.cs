using KSFramework.Domain;

namespace Project.Auth.Domain
{
    public class UserClaim : BaseEntity<Guid>
    {

        public string ClaimType { get; private set; }
        public string ClaimValue { get; private set; }
        
        public Guid UserId { get; private set; }
        public virtual User User { get; protected set; }
        

        public void UpdateClaimValue(string value)
        {
            ClaimValue = value;
        }
        
        public UserClaim(string claimType, string claimValue)
        {
            ClaimType = claimType;
            ClaimValue = claimValue;
        }


        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }

        private UserClaim()
        {
        }
    }
}
