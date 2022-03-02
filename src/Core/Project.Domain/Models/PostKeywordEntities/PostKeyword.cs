using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Common;
using Project.Common.Exceptions;
using Project.Common.Utilities;

namespace Project.Domain.Models.PostKeywordEntities
{
    [Serializable]
    public class PostKeyword : BaseEntity<Guid>, IAggregateRoot, ISerializable
    {

        public PostKeyword(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new AppException(ApiResultStatusCode.BadRequest, "عنوان کلمه کلیدی الزامی است", HttpStatusCode.BadRequest);

            Title = title;
            Name = title.CreateSlug();
        }


        public void Update(string title, string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new AppException(ApiResultStatusCode.BadRequest, "نام لاتین کلمه کلیدی الزامی است", System.Net.HttpStatusCode.BadRequest);
            Name = name;

            if (!string.IsNullOrEmpty(title))
                Title = title;
            else
                Title = name;
        }

        public void SetPostId(Guid postId)
        {
            PostId = postId;
        }

        protected PostKeyword()
        {
        }

        public string Title { get; private set; }
        public string Name { get; private set; }
        public Guid PostId { get; private set; }


        #region Serialization
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Title), Title);
            info.AddValue(nameof(Name), Name);
        }
        #endregion

    }

    public class KeywordConfigurations : IEntityTypeConfiguration<PostKeyword>
    {
        public void Configure(EntityTypeBuilder<PostKeyword> builder)
        {
            builder.ToTable("PostKeywords");
            builder.HasKey(x => x.Id);

        }
    }
}
