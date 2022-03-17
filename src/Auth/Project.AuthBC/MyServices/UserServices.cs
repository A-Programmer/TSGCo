using Microsoft.EntityFrameworkCore;
using Project.Auth.Contracts;
using Project.Auth.Domain;
using Project.Auth.Models;

namespace Project.Auth.Services
{
    public interface IUserServices
    {
        IQueryable<User> GetUsers();
        Task<ResultMessage> ChangeStatus(Guid id);
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

        public async Task<ResultMessage> ChangeStatus(Guid id)
        {
            var rm = new ResultMessage()
            {
                Status = false,
                CssClass = "warning",
                Message = ""
            };
            var user = await _uow.Set<User>().FirstOrDefaultAsync(x => x.Id == id);
            if(user != null)
            {
                if(user.IsActive)
                    user.Deactive();
                else
                    user.Active();

                await _uow.SaveChangesAsync();
                
                rm.CssClass = "success";
                rm.Message = "وضعیت کاربر با موفقیت ویرایش شد.";
                rm.Status = true;
            }
            else
            {
                rm.Message = "کاربر مورد نظر یافت نشد";
            }
            return rm;
        }
    }
}