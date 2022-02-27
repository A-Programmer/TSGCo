using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Domain.Models.UserEntities
{
    [Serializable]
    public class UserProfile : ValueObject, ISerializable
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

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
            yield return ProfileImageUrl;
            yield return AboutMe;
            yield return BirthDate;
            yield return FullName;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(FirstName), FirstName);
            info.AddValue(nameof(LastName), LastName);
            info.AddValue(nameof(ProfileImageUrl), ProfileImageUrl);
            info.AddValue(nameof(AboutMe), AboutMe);
            info.AddValue(nameof(BirthDate), BirthDate);
            info.AddValue(nameof(FullName), FullName);
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
    }



    public class UserProfileConfigurations : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("UsersProfiles");
            builder.HasNoKey();

        }
    }
}
