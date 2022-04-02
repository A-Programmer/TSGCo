using Microsoft.EntityFrameworkCore;
using Project.Auth.Areas.Admin.ViewModels.Users;
using Project.Auth.Data;
using Project.Auth.Domain;
using Project.Auth.Models;

namespace Project.Auth.Services
{
    public interface IUserServices
    {
        IQueryable<User> GetQueryable();

        Task<User> GetById(Guid id);

        Task<ResultMessage> ChangeUserStatus(Guid userId);

        Task<ResultMessage> UpdateUserProfile(UpdateUserProfileViewModel viewModel);

        Task<ResultMessage> AddUserProfile(AddUserProfileViewModel viewModel);

        Task<ResultMessage> RemoveUserProfile(Guid userId);

        Task RemoveUserProfiles(Guid userId);
    }

    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _uow;
        public UserServices(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IQueryable<User> GetQueryable()
        {
            return _uow.Set<User>().AsQueryable();
        }

        public async Task<User> GetById(Guid id)
        {
            return await _uow.Set<User>().Include(x => x.Profile)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ResultMessage> ChangeUserStatus(Guid userId)
        {
            var resultMessage = new ResultMessage(false, "warning", "");

            var user = await _uow.Set<User>()
                .FirstOrDefaultAsync(x => x.Id == userId);

            if(user != null)
            {
                user.ChangeStatus();
                await _uow.SaveChangesAsync();

                resultMessage.Update(true, "success", "عکلیات با موفقیت انجام شد.");

            }
            else
            {
                resultMessage.Update(false, "warning", "گاربر مورد نظر یافت نشد.");
            }

            return resultMessage;
        }

        public async Task<ResultMessage> AddUserProfile(AddUserProfileViewModel viewModel)
        {
            var rm = new ResultMessage(false, "warning", "");

            var user = await _uow.Set<User>().Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == viewModel.UserId);
            if(user != null)
            {
                var userProfile = new UserProfile(viewModel.FirstName, viewModel.LastName);
                user.SetProfile(userProfile);

                await _uow.SaveChangesAsync();

                rm.CssClass = "success";
                rm.Message = "پروفایل کاربر با موفقیت ثبت شد.";
                rm.Status = true;
            }
            else
            {
                rm.CssClass = "warning";
                rm.Message = "کاربر مورد نظر یافت نشد.";
                rm.Status = false;
            }

            return rm;
        }
        
        public async Task<ResultMessage> UpdateUserProfile(UpdateUserProfileViewModel viewModel)
        {
            var rm = new ResultMessage(false, "warning", "");

            var user = await _uow.Set<User>().Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == viewModel.UserId);
            if(user != null)
            {
                if(user.Profile != null)
                {
                    if(string.IsNullOrEmpty(viewModel.FirstName) && string.IsNullOrEmpty(viewModel.LastName))
                    {
                        user.RemoveProfile();
                        await RemoveUserProfiles(viewModel.UserId);
                    }
                    else
                    {
                        var userProfile = await _uow.Set<UserProfile>()
                            .FirstOrDefaultAsync(x => x.Id == user.Profile.Id);
                        userProfile.Update(viewModel.FirstName, viewModel.LastName);
                    }
                    await _uow.SaveChangesAsync();
                }

                rm.CssClass = "success";
                rm.Message = "پروفایل کاربر با موفقیت ویرایش شد.";
                rm.Status = true;
            }
            else
            {
                rm.CssClass = "warning";
                rm.Message = "کاربر مورد نظر یافت نشد.";
                rm.Status = false;
            }

            return rm;
        }
    
        public async Task<ResultMessage> RemoveUserProfile(Guid userId)
        {
            var rm = new ResultMessage(false, "warning", "");

            var user = await _uow.Set<User>().Include(x => x.Profile).FirstOrDefaultAsync(x => x.Id == userId);
            if(user != null)
            {
                if(user.Profile != null)
                {
                    var profile = await _uow.Set<UserProfile>()
                        .FirstOrDefaultAsync(x => x.Id == user.Profile.Id);
                    user.RemoveProfile();
                    _uow.Set<UserProfile>()
                        .Remove(profile);
                    await _uow.SaveChangesAsync();
                }

                rm.CssClass = "success";
                rm.Message = "عملیات با موفقیت انجام شد";
                rm.Status = true;
            }
            else
            {

                rm.CssClass = "warning";
                rm.Message = "کاربر مورد نظر یافت نشد.";
                rm.Status = true;
            }

            return rm;
        }
    
        public async Task RemoveUserProfiles(Guid userId)
        {
            var profiles = await _uow.Set<UserProfile>().Where(x => x.UserId == userId).ToListAsync();
            foreach(var profile in profiles)
            {
                _uow.Set<UserProfile>().Remove(profile);
            }

            await _uow.SaveChangesAsync();
        }

    }
}