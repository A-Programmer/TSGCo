using System.ComponentModel.DataAnnotations.Schema;
using KSFramework.Domain;

namespace Project.Auth.Domain
{
    public class UserProfile : BaseEntity<Guid>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public string FullName
        {
            get
            {
                var fullName = "";

                if (!string.IsNullOrEmpty(this.FirstName))
                    fullName += this.FirstName;

                if (!string.IsNullOrEmpty(this.LastName))
                    fullName += " " + this.LastName;

                return fullName;
            }
        }

        public Guid UserId { get; set; }

        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }
        public void Update(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public UserProfile(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        private UserProfile()
        {

        }
    }
}