using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Shared;
using Project.Domain.Shared.Exceptions;
using Project.Domain.Shared.Utilities;

namespace Project.Domain.Models.PageEntities
{
    [Serializable]
    public class DynamicPage : BaseEntityWithDetails<Guid>, IAggregateRoot, ISerializable
    {
        public DynamicPage(string title, string description, string content, string imageUrl)
        {
            if (string.IsNullOrEmpty(title))
                throw new AppException(ApiResultStatusCode.BadRequest, "عنوان صفحه نمی تواند خالی باشد", HttpStatusCode.BadRequest);

            Title = title;
            Slug = title.CreateSlug();
            Description = description;
            Content = content;
            ImageUrl = imageUrl;
        }


        public void Update(string title, string description, string content, string imageUrl)
        {
            if (string.IsNullOrEmpty(title))
                throw new AppException(ApiResultStatusCode.BadRequest, "عنوان صفحه نمی تواند خالی باشد", HttpStatusCode.BadRequest);

            Title = title;
            Description = description;
            Content = content;
            ImageUrl = imageUrl;
        }


        public void UpdateSlug(string newSlug)
        {
            Slug = newSlug;
        }


        protected DynamicPage()
        {

        }




        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string Description { get; private set; }
        public string Content { get; private set; }
        public string ImageUrl { get; private set; }


        #region Serialization

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Id), Id);
            info.AddValue(nameof(Title), Title);
            info.AddValue(nameof(Slug), Slug);
            info.AddValue(nameof(Description), Description);
            info.AddValue(nameof(Content), Content);
            info.AddValue(nameof(ImageUrl), ImageUrl);

        }
        #endregion

    }

    public class DynamicPageConfigurations : IEntityTypeConfiguration<DynamicPage>
    {
        public void Configure(EntityTypeBuilder<DynamicPage> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
