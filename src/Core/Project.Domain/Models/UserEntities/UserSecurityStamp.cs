using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Common;
using Project.Common.Exceptions;

namespace Project.Domain.Models.UserEntities
{
    [Serializable]
    public class UserSecurityStamp : ValueObject, IEntity, ISerializable
    {
        public UserSecurityStamp(string securityStamp, DateTimeOffset createdDate, DateTimeOffset expirationDate)
        {
            SecurityStamp = securityStamp;
            CreatedAt = createdDate;
            ExpirationDate = expirationDate;
        }

        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }

        public string SecurityStamp { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public DateTimeOffset ExpirationDate { get; private set; }

        public Guid UserId { get; private set; }



        public void UpdateSecurityStamp(string securityStamp)
        {
            if (string.IsNullOrEmpty(securityStamp))
                throw new AppException(ApiResultStatusCode.BadRequest, "مهر امنیتی خالی است", HttpStatusCode.BadRequest);
            SecurityStamp = securityStamp;
        }

        public void UpdateExpirationDate(int daysToAdd)
        {
            ExpirationDate = CreatedAt.AddDays(daysToAdd);
        }


        protected UserSecurityStamp()
        {

        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(SecurityStamp), SecurityStamp);
            info.AddValue(nameof(CreatedAt), CreatedAt);
            info.AddValue(nameof(ExpirationDate), ExpirationDate);
            info.AddValue(nameof(UserId), UserId);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return SecurityStamp;
            yield return CreatedAt;
            yield return ExpirationDate;
            yield return UserId;
        }
    }



    public class UserSecurityStampConfigurations : IEntityTypeConfiguration<UserSecurityStamp>
    {
        public void Configure(EntityTypeBuilder<UserSecurityStamp> builder)
        {
            builder.ToTable("UserSecurityStamps");
            builder.HasNoKey();
        }
    }
}
