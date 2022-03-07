using System;
namespace Project.Application.Dtos.Posts
{
    public class DeleteCommentResponse
    {
        public DeleteCommentResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
