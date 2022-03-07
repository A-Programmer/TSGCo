using System;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Shared;
using Project.Domain.Shared.Exceptions;

namespace Project.Domain.Models.RoleEntities
{
    [Serializable]
    public class Role : BaseEntity<Guid>, IAggregateRoot, ISerializable, IEntity
    {

        public Role(string name, string description = "")
        {
            if (string.IsNullOrEmpty(name))
                throw new AppException(ApiResultStatusCode.BadRequest, "عنوان نقش الزامی است", HttpStatusCode.BadRequest);
            Name = name;
            Description = description;
        }

        public void Update(string name, string description)
        {
            if (string.IsNullOrEmpty(name))
                throw new AppException(ApiResultStatusCode.BadRequest, "عنوان نقش الزامی است", HttpStatusCode.BadRequest);
            Name = name;
            Description = description;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Id), Id);
            info.AddValue(nameof(Name), Name);
            info.AddValue(nameof(Description), Description);
        }

        protected Role()
        {

        }



        public string Name { get; private set; }
        public string Description { get; private set; }
    }

    public class RoleConfigurations : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(x => x.Id);
        }
    }
}
