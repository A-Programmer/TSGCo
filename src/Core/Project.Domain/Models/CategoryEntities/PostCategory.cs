using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Common;
using Project.Common.Exceptions;
using Project.Common.Utilities;
using Project.Domain.Models.PostEntities;

namespace Project.Domain.Models.CategoryEntities
{
    [Serializable]
    public class PostCategory : BaseEntity<Guid>, IAggregateRoot, ISerializable
    {


        public PostCategory(string title, string description = "")
        {
            if (string.IsNullOrEmpty(title))
                throw new AppException(ApiResultStatusCode.BadRequest, "عنوان الزامی است", HttpStatusCode.BadRequest);

            Title = title;
            Slug = title.CreateSlug();
            Description = description;
        }

        protected PostCategory()
        {

        }


        public void Update(string title, string description = "")
        {
            if (string.IsNullOrEmpty(title))
                throw new AppException(ApiResultStatusCode.BadRequest, "عنوان الزامی است", HttpStatusCode.BadRequest);

            Title = title;

            if(!string.IsNullOrEmpty(description))
                Description = description;
        }

        public void UpdateSlug(string slug)
        {
            Slug = slug;
        }

        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string Description { get; private set; }

        #region Serialization
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Id), Id);
            info.AddValue(nameof(Title), Title);
            info.AddValue(nameof(Slug), Slug);
            info.AddValue(nameof(Description), Description);

        }
        #endregion

    }

    public class CategoryConfigurations : IEntityTypeConfiguration<PostCategory>
    {
        public void Configure(EntityTypeBuilder<PostCategory> builder)
        {

            builder.ToTable("Categories");

            builder.HasKey(x => x.Id);

        }
    }
}
