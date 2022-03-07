using System;
namespace Project.Application.Commands.PostCommands
{
    public class EditPostResponse
    {
        public EditPostResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
