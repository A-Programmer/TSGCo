using Project.Auth.Data;
using Project.Auth.Domain;

namespace Project.Auth.Services
{
    public interface IUserServices
    {
        IQueryable<User> GetUsersQueryable();
    }

    public class UserServices : IUserServices
    {
        private readonly ApplicationDbContext _db;
        public UserServices(ApplicationDbContext db)
        {
            _db = db;
        }
        public IQueryable<User> GetUsersQueryable()
        {
            return _db.Users.AsQueryable();
        }
    }
}