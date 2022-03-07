using System;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Shared;
using Project.Domain.Shared.Exceptions;
using Project.Domain.Shared.Utilities;

namespace Project.Domain.Models.ContactUsEntities
{
    [Serializable]
    public class ContactUs : BaseEntityWithDetails<Guid>, IAggregateRoot, ISerializable
    {

        public ContactUs(string fullName, string email, string title, string content)
        {
            if (string.IsNullOrEmpty(fullName))
                throw new AppException(ApiResultStatusCode.BadRequest, "نام کامل الزامی است", HttpStatusCode.BadRequest);

            if (string.IsNullOrEmpty(email))
                throw new AppException(ApiResultStatusCode.BadRequest, "ایمیل الزامی است", HttpStatusCode.BadRequest);

            if (!email.IsValidEmail())
                throw new AppException(ApiResultStatusCode.BadRequest,"ایمیل صحیح وارد نشده است", HttpStatusCode.BadRequest);

            if (string.IsNullOrEmpty(title))
                throw new AppException(ApiResultStatusCode.BadRequest, "عنوان الزامی است", HttpStatusCode.BadRequest);

            if (string.IsNullOrEmpty(title))
                throw new AppException(ApiResultStatusCode.BadRequest, "متن پیام الزامی است", HttpStatusCode.BadRequest);

            FullName = fullName;
            Email = email;
            Title = title;
            Content = content;
        }

        protected ContactUs()
        {

        }

        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }

        #region ISerializable

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Id), Id);
            info.AddValue(nameof(FullName), FullName);
            info.AddValue(nameof(Title), Title);
            info.AddValue(nameof(Content), Content);
        }
        #endregion
    }

    public class ContactUsConfigurations : IEntityTypeConfiguration<ContactUs>
    {
        public void Configure(EntityTypeBuilder<ContactUs> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("ContactUs");
        }
    }
}
