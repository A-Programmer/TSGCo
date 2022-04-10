using System;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Common;
using Project.Common.Exceptions;

namespace Project.Domain.Models.UserEntities
{
    public class UserToken : BaseEntity<Guid>
    {

        public UserToken(TokenTypes type, string token, DateTimeOffset expirationDateTimeOffset)
        {
            Type = type;

            if (string.IsNullOrEmpty(token))
                throw new AppException(ApiResultStatusCode.BadRequest, "Token is required", HttpStatusCode.BadRequest);
            Token = token;
            ExpirationDateTime = expirationDateTimeOffset;
        }

        public TokenTypes Type { get; private set; }
        public string Token { get; private set; }
        public DateTimeOffset ExpirationDateTime { get; private set; }

        
        public Guid UserId { get; private set; }
        public virtual User User { get; protected set; }

        protected UserToken()
        {
        }
    }

    public class UserTokenConfigurations : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("UserTokens");

            builder.HasOne(x => x.User)
                .WithMany(x => x.UserTokens)
                .HasForeignKey(x => x.UserId);
        }
    }
}
