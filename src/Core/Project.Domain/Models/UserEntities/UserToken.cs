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
    public class UserToken : ValueObject, IEntity, ISerializable
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

        public void SetUserId(Guid userId)
        {
            UserId = userId;
        }

        protected UserToken()
        {
        }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Type), Type);
            info.AddValue(nameof(Token), Token);
            info.AddValue(nameof(ExpirationDateTime), ExpirationDateTime);
            info.AddValue(nameof(UserId), UserId);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
            yield return Token;
            yield return ExpirationDateTime;
            yield return UserId;
        }
    }

    public class UserTokenConfigurations : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("UserTokens");
            builder.HasNoKey();

        }
    }
}
