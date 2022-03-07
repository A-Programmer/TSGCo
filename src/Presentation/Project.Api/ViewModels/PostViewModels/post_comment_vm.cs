using System;
using System.Collections.Generic;
using System.Linq;
using Project.Application.Dtos.Posts;

namespace Project.Api.ViewModels.PostViewModels
{
    public class post_comment_vm
    {

        public post_comment_vm(Guid id, string email, string fullName, string content, bool isApproved, Guid postId, Guid? parentId, List<PostCommentDto> replies)
        {
            this.id = id;
            this.email = email;
            this.full_name = fullName;
            this.content = content;
            this.is_approved = isApproved;
            this.post_id = postId;
            this.parent_id = parentId;
            this.replies = replies.Select(x => new post_comment_vm(x.Id, x.Email, x.FullName, x.Content, x.IsApproved, x.PostId, x.ParentId, x.Replies.ToList()));
        }

        public Guid id { get; private set; }
        public string email { get; private set; }
        public string full_name { get; private set; }
        public string content { get; private set; }
        public bool is_approved { get; private set; }
        public Guid post_id { get; private set; }
        public Guid? parent_id { get; private set; }
        public IEnumerable<post_comment_vm> replies { get; private set; }
    }
}
