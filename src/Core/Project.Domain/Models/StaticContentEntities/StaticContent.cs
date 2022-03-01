using System;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Common;
using Project.Common.Exceptions;

namespace Project.Domain.Models.StaticContentEntities
{
    [Serializable]
    public class StaticContent : BaseEntity<Guid>, IAggregateRoot, ISerializable, IEntity
    {
        public StaticContent(string title, string content)
        {
            if (string.IsNullOrEmpty(title))
                throw new AppException(ApiResultStatusCode.BadRequest, "عنوان الزامی است", HttpStatusCode.BadRequest);
            Title = title;
            Content = content;
        }

        public string Title { get; private set; }
        public string Content { get; private set; }

        public void Update(string title, string content)
        {
            if (!string.IsNullOrEmpty(title))
                Title = title;
            Content = content;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Id), Id);
            info.AddValue(nameof(Title), Title);
            info.AddValue(nameof(Content), Content);
        }

        protected StaticContent()
        {

        }
    }


    public class StaticContentConfigurations : IEntityTypeConfiguration<StaticContent>
    {
        public void Configure(EntityTypeBuilder<StaticContent> builder)
        {
            builder.ToTable("StaticContents");

            builder.HasKey(x => x.Id);
        }
    }
}
