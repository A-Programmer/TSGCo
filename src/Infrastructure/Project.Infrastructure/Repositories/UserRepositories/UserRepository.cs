using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Domain.Contracts.Repositories.UserRepositories;
using Project.Domain.Models.UserEntities;

namespace Project.Infrastructure.Repositories.UserRepositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ProjectDbContext db)
            : base(db)
        {
        }

        public async Task<User> GetUserProfile(Guid id)
        {
            return await Entity
                .Include(x => x.Profile)
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserWithRolesById(Guid id)
        {
            var query = Entity
                .Include(x => x.Roles);

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserWithRolesByUserName(string username)
        {
            return await Entity.Include(x => x.SecurityStamps)
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower());
        }

        public async Task<IEnumerable<User>> GetAllUsersWithRoles()
        {
            return await Entity.Include(x => x.Roles).ToListAsync();
        }





        #region Validating User

        public async Task<User> GetUserByUserNameAndPassword(string userName, string hashedPassword)
        {
            return await Entity.Include(x => x.Roles).FirstOrDefaultAsync(x => x.UserName.ToLower() == userName.ToLower() && x.HashedPassword == hashedPassword);
        }

        public async Task<User> GetUserByEmailAndPassword(string email, string hashedPassword)
        {
            return await Entity.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower() && x.HashedPassword == hashedPassword);
        }

        public async Task<User> GetUserByMobileAndPassword(string mobile, string hashedPassword)
        {
            return await Entity.Include(x => x.Roles).FirstOrDefaultAsync(x => x.PhoneNumber.ToLower() == mobile.ToLower() && x.HashedPassword == hashedPassword);
        }

        #endregion


        public async Task<User> GetUserDashboardData(Guid id)
        {
            return await Entity.Include(x => x.Roles)
                .Include(x => x.Profile)
                .Include(x => x.LoginDates.Take(5))
                .FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<User> GetUserByIdAndUserName(Guid id, string userName)
        {
            return await Entity.Include(x => x.Profile)
                .FirstOrDefaultAsync(x => x.Id == id && x.UserName.ToLower() == userName.ToLower());
        }


        public async Task<User> GetUserBySecurityStamp(string securityStamp)
        {
            return await Entity
                .Include(x => x.SecurityStamps)
                .FirstOrDefaultAsync(x => x.SecurityStamps.FirstOrDefault(s => s.SecurityStamp == securityStamp && s.ExpirationDate > DateTimeOffset.Now) != null);
        }

        public async Task<User> GetUserByRefreshToken(string refreshToken)
        {
            return await Entity.Include(x => x.UserTokens).FirstOrDefaultAsync(x => x.UserTokens.Any(t => t.Token == refreshToken && t.ExpirationDateTime > DateTimeOffset.Now));
        }

        public async Task<bool> IsActiveUser(Guid id)
        {
            var user = await Entity.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
                return user.IsActive;
            return false;
        }

        public async Task<bool> IsSuperUser(Guid id)
        {
            var user = await Entity.FirstOrDefaultAsync(x => x.Id == id);
            if (user != null)
                return user.IsSuperAdmin;
            return false;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await Entity.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Email.ToLower() == email);
        }

        public async Task<User> GetUserByName(string username)
        {
            return await Entity.Include(x => x.Roles).FirstOrDefaultAsync(x => x.UserName.ToLower() == username);
        }

        public async Task<User> GetUserByPhone(string phone)
        {
            return await Entity.Include(x => x.Roles).FirstOrDefaultAsync(x => x.PhoneNumber.ToLower() == phone);
        }

        public async Task<User> GetUserWithRelationsById(Guid id)
        {
            return await Entity
                .Include(x => x.UserTokens)
                .Include(x => x.Roles)
                .Include(x => x.Posts)
                .FirstOrDefaultAsync(x => x.Id == id);
        }



    }
}
