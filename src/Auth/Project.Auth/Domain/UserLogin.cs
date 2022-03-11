using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KSFramework.Domain;

namespace Project.Auth.Domain
{
    public class UserLogin : BaseEntity<Guid>
    {
        public string UserId { get; set; }
        
        public User User { get; set; }

        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }


        public void SetUserId(string userId)
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
}
