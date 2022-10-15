 using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Common;
using Project.Common.Exceptions;
using Project.Common.Utilities;
using Project.Domain.Models.RoleEntities;

namespace Project.Domain.Models.UserEntities
{
    public class User : BaseEntity<Guid>, IAggregateRoot
    {

        public User(Guid id)
        {
            Id = id;
        }

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

        public virtual IReadOnlyCollection<Role> Roles => _roles;
        protected List<Role> _roles = new List<Role>();

        public virtual IReadOnlyCollection<UserToken> UserTokens => _userTokens;
        protected List<UserToken> _userTokens = new List<UserToken>();

        //public virtual IReadOnlyCollection<Post> Posts => _posts;
        //protected List<Post> _posts = new List<Post>();


        public virtual IReadOnlyCollection<UserLoginDate> UserLoginDates => _loginDates;
        protected List<UserLoginDate> _loginDates = new List<UserLoginDate>();

        public virtual IReadOnlyCollection<UserSecurityStamp> UserSecurityStamps => _securityStamps;
        protected List<UserSecurityStamp> _securityStamps = new List<UserSecurityStamp>();


        public virtual UserProfile Profile { get; protected set; }



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
                throw new AppException(ApiResultStatusCode.BadRequest, "ایمیل الزامی است", HttpStatusCode.BadRequest);

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

        public void SetId(Guid id)
        {
            Id = id;
        }

        public void AddUserToRole(Role role)
        {
            _roles.Add(role);
        }

        public void RemoveUserRole(Role role)
        {
            _roles.Remove(role);
        }

        public void RemoveUserRoles()
        {
            _roles.Clear();
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
        public void AddProfile(UserProfile profile)
        {
            Profile = profile;
        }

        public void UpdateProfile(UserProfile profile)
        {
            Profile.Update(profile.FirstName, profile.LastName, profile.ProfileImageUrl, profile.AboutMe, profile.BirthDate);
            Profile.SetUserId(profile.UserId);
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
