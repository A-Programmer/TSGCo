using Project.Auth.Contracts;
using Project.Auth.Domain;

namespace Project.Auth.Services
{
    public interface IUserServices
    {
        IQueryable<User> GetUsers();
    }

    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _uow;
        public UserServices(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IQueryable<User> GetUsers()
        {
            return _uow.Set<User>().AsQueryable();
        }
    }
}