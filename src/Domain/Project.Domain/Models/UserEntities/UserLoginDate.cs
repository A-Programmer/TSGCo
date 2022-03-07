using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Domain.Models.UserEntities
{
    [Serializable]
    public class UserLoginDate : ValueObject, IEntity, ISerializable
    {
        public UserLoginDate(DateTimeOffset loginDate, string ipAddress = "")
        {
            LoginDate = loginDate;
            IpAddress = ipAddress;
        }


        public UserLoginDate(DateTimeOffset loginDate, Guid userId, string ipAddress = "")
        {
            LoginDate = loginDate;
            IpAddress = ipAddress;
            UserId = userId;
        }

        public DateTimeOffset LoginDate { get; private set; }
        public string IpAddress { get; private set; }

        public Guid UserId { get; private set; }


        protected UserLoginDate()
        {

        }

        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return LoginDate;
            yield return IpAddress;
            yield return UserId;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(LoginDate), LoginDate);
            info.AddValue(nameof(IpAddress), IpAddress);
            info.AddValue(nameof(UserId), UserId);
        }
    }



    public class UserLoginDateConfigurations : IEntityTypeConfiguration<UserLoginDate>
    {
        public void Configure(EntityTypeBuilder<UserLoginDate> builder)
        {
            builder.ToTable("UserLoginDates");
            builder.HasNoKey();
        }
    }

}
