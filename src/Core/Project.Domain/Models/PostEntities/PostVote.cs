using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Domain.Models.PostEntities
{
    public class PostVote : ValueObject, ISerializable, IEntity
    {
        public DateTimeOffset CreatedDate { get; private set; }
        public string UserIp { get; private set; }

        public Guid? PostId { get; private set; }

        protected PostVote()
        {
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(CreatedDate), CreatedDate);
            info.AddValue(nameof(UserIp), UserIp);
            info.AddValue(nameof(PostId), PostId);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CreatedDate;
            yield return UserIp;
            yield return PostId;
        }
    }

    public class PostVotesConfigurations : IEntityTypeConfiguration<PostVote>
    {
        public void Configure(EntityTypeBuilder<PostVote> builder)
        {
            builder.ToTable("PostVotes");
            builder.HasNoKey();

        }
    }
}
