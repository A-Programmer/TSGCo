using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Domain.Models.CategoryEntities;

namespace Project.Domain.Models.PostEntities
{
    [Serializable]
    public class PostsCategories : ValueObject, ISerializable
    {
        public PostsCategories(Guid postId, Guid categoryId)
        {
            PostId = postId;
            CategoryId = categoryId;
        }

        protected PostsCategories()
        {
        }

        public Guid PostId { get; private set; }
        [ForeignKey(nameof(PostId))]
        public Post Post { get; private set; }

        public Guid CategoryId { get; private set; }


        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(PostId), PostId);
            info.AddValue(nameof(CategoryId), CategoryId);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return PostId;
            yield return CategoryId;
        }
    }

    //public class PostsCategoriesConfigurations : IEntityTypeConfiguration<PostsCategories>
    //{
    //    public void Configure(EntityTypeBuilder<PostsCategories> builder)
    //    {
    //        builder.ToTable("PostsCategories");
    //        builder.HasNoKey();
    //    }
    //}
}
