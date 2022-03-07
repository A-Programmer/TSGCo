using System;
namespace Project.Application.Dtos.Posts
{
    public class AddCommentResponse
    {
        public AddCommentResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
