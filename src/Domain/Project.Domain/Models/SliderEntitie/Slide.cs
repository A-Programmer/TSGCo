using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Shared;
using Project.Domain.Shared.Exceptions;
using System;
using System.Net;
using System.Runtime.Serialization;

namespace Project.Domain.Models.SliderEntitie
{
    [Serializable]
    public class Slide : BaseEntity<Guid>, IAggregateRoot, ISerializable, IEntity
    {
        public int AppearanceOrder { get; private set; }
        public string ImageUrl { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string ButtunText { get; private set; }
        public string ButtunUrl { get; private set; }
        public string SecondaryButtunText { get; private set; }
        public string SecondaryButtunUrl { get; private set; }
        public bool Status { get; private set; }

        public Slide(string imageUrl, string title = null, string description = null, string buttunText = null, string buttunUrl = null,
            string secondaryButtunText = null, string secondaryButtunUrl = null, bool status = false, int appearanceOrder = 0)
        {
            if (string.IsNullOrEmpty(imageUrl))
                throw new AppException(ApiResultStatusCode.BadRequest, "تصویر الزامی است", HttpStatusCode.BadRequest);

            AppearanceOrder = appearanceOrder;
            ImageUrl = imageUrl;
            Title = title;
            Description = description;
            ButtunText = buttunText;
            ButtunUrl = buttunUrl;
            SecondaryButtunText = secondaryButtunText;
            SecondaryButtunUrl = secondaryButtunUrl;
            Status = status;
        }

        protected Slide()
        {

        }

        public void UpdateOrder(int order)
        {
            AppearanceOrder = AppearanceOrder;
        }

        public void ChangeStatus()
        {
            Status = !Status;
        }

        public void Update(string imageUrl, string title, string description, string buttunText = null, string buttunUrl = null,
            string secondaryButtunText = null, string secondaryButtunUrl = null, bool status = false, int appearanceOrder = 0)
        {
            if (string.IsNullOrEmpty(imageUrl))
                throw new AppException(ApiResultStatusCode.BadRequest, "تصویر الزامی است", HttpStatusCode.BadRequest);

            AppearanceOrder = appearanceOrder;
            ImageUrl = imageUrl;
            Title = title;
            Description = description;
            ButtunText = buttunText;
            ButtunUrl = buttunUrl;
            SecondaryButtunText = secondaryButtunText;
            SecondaryButtunUrl = secondaryButtunUrl;
            Status = status;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Id), Id);
            info.AddValue(nameof(AppearanceOrder), AppearanceOrder);
            info.AddValue(nameof(ImageUrl), ImageUrl);
            info.AddValue(nameof(Title), Title);
            info.AddValue(nameof(Description), Description);
            info.AddValue(nameof(ButtunText), ButtunText);
            info.AddValue(nameof(ButtunUrl), ButtunUrl);
            info.AddValue(nameof(SecondaryButtunText), SecondaryButtunText);
            info.AddValue(nameof(SecondaryButtunUrl), SecondaryButtunUrl);
            info.AddValue(nameof(Status), Status);
        }
    }

    public class ContactUsConfigurations : IEntityTypeConfiguration<Slide>
    {
        public void Configure(EntityTypeBuilder<Slide> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ImageUrl).IsRequired();


        }
    }
}
