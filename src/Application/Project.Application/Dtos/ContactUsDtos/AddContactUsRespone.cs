using System;
namespace Project.Application.Dtos.ContactUsDtos
{
    public class AddContactUsRespone
    {
        public AddContactUsRespone(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
