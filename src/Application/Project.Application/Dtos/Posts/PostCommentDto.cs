using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Project.Domain.Models.PostEntities;

namespace Project.Application.Dtos.Posts
{
    public class PostCommentDto
    {
        public PostCommentDto(Guid id, string email, string fullName, string content, bool isApproved, Guid postId, Guid? parentId, List<PostComment> postComments)
        {
            Id = id;
            Email = email;
            FullName = fullName;
            Content = content;
            IsApproved = isApproved;
            PostId = postId;
            ParentId = parentId;
            Replies = postComments.Select(x => new PostCommentDto(x.Id, x.Email, x.FullName, x.Content, x.IsApproved, x.PostId, x.ParentId, x.Replies.ToList()));
        }

        public Guid Id { get; private set; }
        public string Email { get; private set; }
        public string FullName { get; private set; }
        public string Content { get; private set; }
        public bool IsApproved { get; private set; }
        public Guid PostId { get; private set; }
        public Guid? ParentId { get; private set; }
        public IEnumerable<PostCommentDto> Replies { get; private set; }

    }
}
