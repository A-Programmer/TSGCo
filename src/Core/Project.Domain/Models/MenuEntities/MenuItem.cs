using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project.Domain.Models.MenuEntities
{
    [Serializable]
    public class MenuItem : BaseEntity<Guid>, IAggregateRoot, ISerializable
    {

        public string Title { get; set; }

        public string Url { get; set; }

        public int Order { get; set; }

        public Guid? ParentId { get; set; }


        public MenuItem(string title, string url, int order)
        {
            Title = title;
            Url = url;
            Order = order;
        }

        public void SetParentId(Guid? parentId)
        {
            ParentId = parentId;
        }

        public void Update(string title, string url, int order)
        {
            Title = title;
            Url = url;
            Order = order;
        }

        protected MenuItem()
        {
        }

        #region Serialization

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Id), Id);
            info.AddValue(nameof(Title), Title);
            info.AddValue(nameof(Url), Url);
            info.AddValue(nameof(Order), Order);
            info.AddValue(nameof(ParentId), ParentId);
        }
        #endregion
    }

    public class MenuItemConfigurations : IEntityTypeConfiguration<MenuItem>
    {
        public void Configure(EntityTypeBuilder<MenuItem> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
