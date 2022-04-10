using System;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Common;
using Project.Common.Exceptions;

namespace Project.Domain.Models.UserEntities
{
    public class UserSecurityStamp : BaseEntity<Guid>
    {
        public UserSecurityStamp(string securityStamp, DateTimeOffset createdDate, DateTimeOffset expirationDate)
        {
            SecurityStamp = securityStamp;
            CreatedAt = createdDate;
            ExpirationDate = expirationDate;
        }


        public UserSecurityStamp(Guid id, string securityStamp, DateTimeOffset createdDate, DateTimeOffset expirationDate)
        {
            Id = id;
            SecurityStamp = securityStamp;
            CreatedAt = createdDate;
            ExpirationDate = expirationDate;
        }


        public UserSecurityStamp(Guid id, string securityStamp, DateTimeOffset createdDate, DateTimeOffset expirationDate, Guid userId)
        {
            Id = id;
            SecurityStamp = securityStamp;
            CreatedAt = createdDate;
            ExpirationDate = expirationDate;
            UserId = userId;
        }


        public UserSecurityStamp(string securityStamp, DateTimeOffset createdDate, DateTimeOffset expirationDate, Guid userId)
        {
            SecurityStamp = securityStamp;
            CreatedAt = createdDate;
            ExpirationDate = expirationDate;
            UserId = userId;
        }


        public string SecurityStamp { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public DateTimeOffset ExpirationDate { get; private set; }


        public Guid UserId { get; private set; }
        public virtual User User { get; protected set; }

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


        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }




    }



    public class UserSecurityStampConfigurations : IEntityTypeConfiguration<UserSecurityStamp>
    {
        public void Configure(EntityTypeBuilder<UserSecurityStamp> builder)
        {
            builder.ToTable("UserSecurityStamps");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserSecurityStamps)
                .HasForeignKey(x => x.UserId);
        }
    }
}
