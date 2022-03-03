using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Project.Common.Exceptions;

namespace Project.Domain.Models.PostEntities
{
    [Serializable]
    public class PostComment : BaseEntity<Guid>, ISerializable
    {
        public string Email { get; private set; }
        public string FullName { get; private set; }
        public string Content { get; private set; }
        public bool IsApproved { get; private set; }

        private List<PostComment> _replies = new List<PostComment>();
        public IReadOnlyCollection<PostComment> Replies
        {
            get { return _replies.AsReadOnly(); }
        }

        public Guid? ParentId { get; private set; }

        public Guid PostId { get; private set; }
        public Post Post { get; private set; }


        public void SetParentId(Guid? parentId)
        {
            ParentId = parentId;
        }

        public void SetPostId(Guid postId)
        {
            PostId = postId;
        }

        public void AddReply(PostComment reply)
        {
            _replies.Add(reply);
        }

        public void ChangeStatus(bool isApproved)
        {
            IsApproved = isApproved;
        }

        public void SetReplies(List<PostComment> postComment)
        {
            _replies = postComment;
        }

        public PostComment(string email, string fullName, string content, bool isApproved)
        {
            if (string.IsNullOrEmpty(email))
                throw new AppException(Project.Common.ApiResultStatusCode.BadRequest, "ایمیل الزامی است", System.Net.HttpStatusCode.BadRequest);

            if (string.IsNullOrEmpty(content))
                throw new AppException(Project.Common.ApiResultStatusCode.BadRequest, "متن نظر الزامی است", System.Net.HttpStatusCode.BadRequest);

            Email = email;
            FullName = fullName;
            Content = content;
            IsApproved = isApproved;
        }

        #region Serialization

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Id), Id);
            info.AddValue(nameof(Email), Email);
            info.AddValue(nameof(FullName), FullName);
            info.AddValue(nameof(Content), Content);
            info.AddValue(nameof(IsApproved), IsApproved);
            info.AddValue(nameof(ParentId), ParentId);
            info.AddValue(nameof(PostId), PostId);
            info.AddValue(nameof(Replies ), Replies);
        }
        #endregion
    }

    public class PostCommentConfigurations : IEntityTypeConfiguration<PostComment>
    {
        public void Configure(EntityTypeBuilder<PostComment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email)
                .IsRequired();

            builder.Property(x => x.Content)
                .IsRequired();

        }
    }
}
