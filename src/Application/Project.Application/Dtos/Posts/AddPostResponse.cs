using System;
namespace Project.Application.Commands.PostCommands
{
    public class AddPostResponse
    {
        public AddPostResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
