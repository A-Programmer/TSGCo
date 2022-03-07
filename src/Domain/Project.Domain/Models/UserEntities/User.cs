 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Shared;
using Project.Domain.Shared.Exceptions;
using Project.Domain.Shared.Utilities;
using Project.Domain.Models.PostEntities;

namespace Project.Domain.Models.UserEntities
{
    [Serializable]
    public class User : BaseEntity<Guid>, IAggregateRoot, ISerializable
    {

        public User(string userName, string passwordHash, string email, string phoneNumber = "", bool isActive = false, bool isSuperAdmin = false)
        {
            if (string.IsNullOrEmpty(userName))
                throw new AppException(ApiResultStatusCode.BadRequest, "نام کاربری الزامی است", HttpStatusCode.BadRequest);

            UserName = userName;

            if (string.IsNullOrEmpty(passwordHash))
                throw new AppException(ApiResultStatusCode.BadRequest, "کلمه عبور الزامی است", HttpStatusCode.BadRequest);

            HashedPassword = passwordHash;

            if (string.IsNullOrEmpty(email))
                throw new AppException(ApiResultStatusCode.BadRequest, "ایمیل الزامی است", HttpStatusCode.BadRequest);

            if(!email.IsValidEmail())
                throw new AppException(ApiResultStatusCode.BadRequest, "ایمیل صحیح نمی باشد", HttpStatusCode.BadRequest);

            Email = email;

            if (!string.IsNullOrEmpty(phoneNumber))
                PhoneNumber = phoneNumber;

            RegisteredAt = DateTimeOffset.Now;
            IsActive = isActive;
            IsSuperAdmin = isSuperAdmin;
        }


        public string UserName { get; private set; }
        public string HashedPassword { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public DateTimeOffset RegisteredAt { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsSuperAdmin { get; private set; }
        

        private List<UserRole> _roles = new List<UserRole>();
        [NotMapped]
        public IReadOnlyCollection<UserRole> Roles
        {
            get { return _roles.AsReadOnly(); }
        }

        private List<UserToken> _userTokens = new List<UserToken>();
        [NotMapped]
        public IReadOnlyCollection<UserToken> UserTokens
        {
            get { return _userTokens.AsReadOnly(); }
        }

        private List<Post> _posts = new List<Post>();
        [NotMapped]
        public IReadOnlyCollection<Post> Posts
        {
            get { return _posts.AsReadOnly(); }
        }


        private List<UserLoginDate> _loginDates = new List<UserLoginDate>();
        [NotMapped]
        public IReadOnlyCollection<UserLoginDate> LoginDates
        {
            get { return _loginDates.AsReadOnly(); }
        }

        private List<UserSecurityStamp> _securityStamps = new List<UserSecurityStamp>();
        [NotMapped]
        public IReadOnlyCollection<UserSecurityStamp> SecurityStamps
        {
            get { return _securityStamps.AsReadOnly(); }
        }


        [NotMapped]
        public virtual UserProfile? Profile { get; protected set; }



        public void Update(string email, string phoneNumber, UserSecurityStamp securityStamp = null)
        {
            if (!string.IsNullOrEmpty(email))
                Email = email;

            if (!string.IsNullOrEmpty(phoneNumber))
                PhoneNumber = phoneNumber;

            if(securityStamp != null)
            {
                DeactiveSecurityStamps();
                UpdateSecurityStamp(securityStamp);
            }
        }

        public void ChangeStatus(bool isActive)
        {
            IsActive = isActive;
        }

        public void ChangeSuperAdminStatus(bool isSuperAdmin)
        {
            IsSuperAdmin = isSuperAdmin;
        }

        public void UpdateSecurityStamp(UserSecurityStamp securityStamp)
        {
            DeactiveSecurityStamps();

            _securityStamps.Add(securityStamp);
        }

        public void ChangePassword(string hashedPassword, UserSecurityStamp securityStamp)
        {
            if (string.IsNullOrEmpty(hashedPassword))
                throw new AppException(ApiResultStatusCode.BadRequest, "کلمه عبور الزامی است", HttpStatusCode.BadRequest);
            HashedPassword = hashedPassword;

            DeactiveSecurityStamps();

            UpdateSecurityStamp(securityStamp);

        }

        public void DeactiveSecurityStamps()
        {
            var securityStamps = _securityStamps.ToList();
            foreach (var securityStamp in securityStamps)
                securityStamp.UpdateExpirationDate(0);
        }

        public void ChangeEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
                throw new AppException(ApiResultStatusCode.BadRequest, "ایمیل ابزامی است", HttpStatusCode.BadRequest);

            if (!email.IsValidEmail())
                throw new AppException(ApiResultStatusCode.BadRequest, "ایمیل صحیح نمی باشد", HttpStatusCode.BadRequest);

            Email = email;
        }

        public void ChangePhoneNumber(string phoneNumber)
        {
            if (!string.IsNullOrEmpty(phoneNumber))
                throw new AppException(ApiResultStatusCode.BadRequest, "شماره تماس الزامی است", HttpStatusCode.BadRequest);

            if (!phoneNumber.IsValidMobile())
                throw new AppException(ApiResultStatusCode.BadRequest, "شماره موبایل صحیح نمی باشد", HttpStatusCode.BadRequest);

            PhoneNumber = phoneNumber;
        }

        public void UpdateUserRoles(Guid[] rolesIds)
        {
            _roles.Clear();

            foreach(var roleId in rolesIds)
                _roles.Add(new UserRole(this.Id, roleId));
        }

        public void RemoveToken(UserToken token)
        {
            _userTokens.Remove(token);
        }

        public void AddToken(UserToken token)
        {
            _userTokens.Add(token);
        }

        #region Profile
        public void UpdateProfile(UserProfile profile)
        {
            if (Profile == null)
            {
                Profile = profile;
            }
            else
            {
                Profile.Update(profile.FirstName, profile.LastName, profile.ProfileImageUrl, profile.AboutMe, profile.BirthDate);
                Profile.SetUserId(profile.UserId);
            }
        }

        #endregion

        #region LoginDates


        public void UpdateLastLoginDate(UserLoginDate loginDate)
        {
            _loginDates.Add(loginDate);
        }

        public void RemoveLoginDate(UserLoginDate loginDate)
        {
            _loginDates.Remove(loginDate);
        }

        #endregion

        #region SecurityStamps

        public UserSecurityStamp GetLatestActiveSecurityStamp()
        {
            return _securityStamps.LastOrDefault(x => x.ExpirationDate > DateTimeOffset.Now);
        }

        public void AddSecurityStamp(UserSecurityStamp securityStamp)
        {
            _securityStamps.Add(securityStamp);
        }

        public void RemoveSecurityStamp(UserSecurityStamp securityStamp)
        {
            _securityStamps.Remove(securityStamp);
        }

        #endregion

        protected User()
        {

        }


        #region Serialization
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(UserName), UserName);
            info.AddValue(nameof(HashedPassword), HashedPassword);
            info.AddValue(nameof(Email), Email);
            info.AddValue(nameof(PhoneNumber), PhoneNumber);
            info.AddValue(nameof(RegisteredAt), RegisteredAt);
            info.AddValue(nameof(IsActive), IsActive);
            info.AddValue(nameof(IsSuperAdmin), IsSuperAdmin);
            info.AddValue(nameof(Roles), Roles);
            info.AddValue(nameof(UserTokens), UserTokens);
            info.AddValue(nameof(Posts), Posts);
            info.AddValue(nameof(LoginDates), LoginDates);
            info.AddValue(nameof(SecurityStamps), SecurityStamps);
            info.AddValue(nameof(Profile), Profile);
        }
        #endregion

    }

    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

        }
    }
}
