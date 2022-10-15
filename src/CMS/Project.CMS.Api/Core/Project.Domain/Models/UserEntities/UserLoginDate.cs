using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Domain.Models.UserEntities
{
    public class UserLoginDate : BaseEntity<Guid>
    {
        public UserLoginDate(DateTimeOffset loginDate, string ipAddress = "")
        {
            LoginDate = loginDate;
            IpAddress = ipAddress;
        }

        public UserLoginDate(Guid id, DateTimeOffset loginDate, string ipAddress = "")
        {
            Id = id;
            LoginDate = loginDate;
            IpAddress = ipAddress;
        }

        public UserLoginDate(DateTimeOffset loginDate, Guid userId, string ipAddress = "")
        {
            LoginDate = loginDate;
            IpAddress = ipAddress;
            UserId = userId;
        }

        public UserLoginDate(Guid id, DateTimeOffset loginDate, Guid userId, string ipAddress = "")
        {
            Id = id;
            LoginDate = loginDate;
            IpAddress = ipAddress;
            UserId = userId;
        }

        public DateTimeOffset LoginDate { get; private set; }
        public string IpAddress { get; private set; }

        public Guid UserId { get; private set; }
        public virtual User User { get; protected set; }


        protected UserLoginDate()
        {

        }

        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }
    }



    public class UserLoginDateConfigurations : IEntityTypeConfiguration<UserLoginDate>
    {
        public void Configure(EntityTypeBuilder<UserLoginDate> builder)
        {
            builder.ToTable("UserLoginDates");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserLoginDates)
                .HasForeignKey(x => x.UserId);
        }
    }

}
