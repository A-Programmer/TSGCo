using System;
using Project.Domain.Contracts.Repositories.ContactUsRepositories;
using Project.Domain.Models.ContactUsEntities;

namespace Project.Infrastructure.Repositories.ContactUsRepositories
{
    public class ContactUsRepository : Repository<ContactUs>, IContactUsRepository
    {
        public ContactUsRepository(ProjectDbContext db)
            : base(db)
        {
        }
    }
}
