using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Domain.Models.UserEntities
{
    public class UserProfile : BaseEntity<Guid>
    {
        public UserProfile(string firstName, string lastName, string profileImageUrl, string aboutMe, DateTimeOffset? birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            ProfileImageUrl = profileImageUrl;
            AboutMe = aboutMe;
            BirthDate = birthDate;
        }


        public void Update(string firstName, string lastName, string profileImageUrl, string aboutMe, DateTimeOffset? birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            ProfileImageUrl = profileImageUrl;
            AboutMe = aboutMe;
            BirthDate = birthDate;

        }

        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }


        protected UserProfile()
        {

        }



        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string ProfileImageUrl { get; private set; }
        public string AboutMe { get; private set; }
        public DateTimeOffset? BirthDate { get; private set; }

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

        public Guid UserId { get; private set; }
        public virtual User User { get; protected set; }
    }



    public class UserProfileConfigurations : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("UsersProfile");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
                .WithOne(x => x.Profile)
                .HasForeignKey<UserProfile>(x => x.UserId);
        }
    }
}
