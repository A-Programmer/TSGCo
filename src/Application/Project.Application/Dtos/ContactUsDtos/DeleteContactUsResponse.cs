using System;
namespace Project.Application.Dtos.ContactUsDtos
{
    public class DeleteContactUsResponse
    {
        public DeleteContactUsResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
