using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Project.Auth.Domain
{
    public class User : IdentityUser<Guid>
    {
        public bool IsActive { get; set; } = true;
        public bool IsSuperAdmin { get; set; } = false;

        public string Name
        {
            get
            {
                return (this.Profile == null || string.IsNullOrEmpty(this.Profile?.FullName)) ? this.UserName : this.Profile.FullName;
            }
        }
        public void SetProfile(UserProfile profile)
        {
            if(Profile != null)
                Profile = null;
                
            Profile = profile;
        }

        public void RemoveProfile()
        {
            Profile = null;
        }
        public UserProfile? Profile { get; protected set; }
        [ForeignKey("ProfileId")]
        protected Guid? ProfileId { get; set; }
    }
}