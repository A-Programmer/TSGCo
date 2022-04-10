using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Common;
using Project.Common.Exceptions;
using Project.Domain.Models.UserEntities;

namespace Project.Domain.Models.RoleEntities
{
    public class Role : BaseEntity<Guid>, IAggregateRoot
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

        public void AddUserToRole(User user)
        {
            _users.Add(user);
        }

        public void RemoveUserFromRole(User user)
        {
            _users.Remove(user);
        }

        public void SetId(Guid id)
        {
            Id = id;
        }

        protected Role()
        {

        }



        public string Name { get; private set; }
        public string Description { get; private set; }

        public virtual IReadOnlyCollection<User> Users => _users;
        protected List<User> _users = new List<User>();
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
