using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Domain.Models.UserEntities;

namespace Project.Domain.Contracts.Repositories.UserRepositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserProfile(Guid id);

        #region Validating User
        Task<User> GetUserByUserNameAndPassword(string userName, string hashedPassword);
        Task<User> GetUserByEmailAndPassword(string email, string hashedPassword);
        Task<User> GetUserByMobileAndPassword(string mobile, string hashedPassword);
        #endregion


        Task<User> GetUserByIdAndUserName(Guid id, string userName);

        Task<bool> IsActiveUser(Guid id);

        Task<bool> IsSuperUser(Guid id);

        Task<User> GetUserByEmail(string email);

        Task<User> GetUserByName(string username);

        Task<User> GetUserByPhone(string phone);

        Task<User> GetUserWithRolesById(Guid id);

        Task<User> GetUserWithRolesByUserName(string username);

        Task<User> GetUserBySecurityStamp(string securityStamp);

        Task<User> GetUserByRefreshToken(string refreshToken);

        Task<IEnumerable<User>> GetAllUsersWithRoles();

        Task<User> GetUserWithRelationsById(Guid id);

    }
}
