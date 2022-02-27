using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Common;
using Project.Common.Exceptions;
using Project.Common.Utilities;

namespace Project.Domain.Models.AnnouncementEntities
{
    [Serializable]
    public class Announcement : BaseEntityWithDetails<Guid>, IAggregateRoot, ISerializable
    {

        public Announcement(string title, string description, string content)
        {

            if (string.IsNullOrEmpty(title))
                throw new AppException(ApiResultStatusCode.BadRequest, "عنوان الزامی است", HttpStatusCode.BadRequest);

            if (string.IsNullOrEmpty(content))
                throw new AppException(ApiResultStatusCode.BadRequest, "متن الزامی است", HttpStatusCode.BadRequest);

            Title = title;
            Slug = title.CreateSlug();
            Description = description;
            Content = content;
        }

        public void Update(string title, string description, string content)
        {
            if (string.IsNullOrEmpty(title))
                throw new AppException(ApiResultStatusCode.BadRequest, "عنوان الزامی است", HttpStatusCode.BadRequest);

            if (string.IsNullOrEmpty(content))
                throw new AppException(ApiResultStatusCode.BadRequest, "متن الزامی است", HttpStatusCode.BadRequest);

            Title = title;
            Slug = title.CreateSlug();
            Description = description;
            Content = content;
        }

        protected Announcement()
        {

        }


        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string Description { get; private set; }
        public string Content { get; private set; }

        #region Serialization
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Id), Id);
            info.AddValue(nameof(Title), Title);
            info.AddValue(nameof(Slug), Slug);
            info.AddValue(nameof(Description), Description);
            info.AddValue(nameof(Content), Content);
        }
        #endregion
    }

    public class AnnouncementConfigurations : IEntityTypeConfiguration<Announcement>
    {
        public void Configure(EntityTypeBuilder<Announcement> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.ToTable("Announcements");
        }
    }
}
