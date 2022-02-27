using System;
namespace Project.Application.Dtos.Posts
{
    public class DeletePostResponse
    {
        public DeletePostResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
