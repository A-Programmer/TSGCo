using Project.Application.Contracts.Repositories.ContactUsRepositories;
using Project.Domain.Models.ContactUsEntities;
using Project.EntityFrameworkCore;


namespace Project.EntityFrameworkCore.Repositories.ContactUsRepositories
{
    public class ContactUsRepository : Repository<ContactUs>, IContactUsRepository
    {
        public ContactUsRepository(ProjectDbContext db)
            : base(db)
        {
        }
    }
}
