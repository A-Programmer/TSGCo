using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Auth.Domain;
using Project.Auth.Utilities;
using Project.Auth.Contracts;

namespace Project.Auth.Services
{

    public interface IUsersService
    {
        Task<bool> AreUserCredentialsValidAsync(string username, string password);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByProviderAsync(string loginProvider, string providerKey);
        Task<User> GetUserByIdAsync(Guid subjectId);
        Task<User> GetUserByUsernameAsync(string username);
        Task<IEnumerable<UserClaim>> GetUserClaimsBySubjectIdAsync(Guid subjectId);
        Task<IEnumerable<UserLogin>> GetUserLoginsBySubjectIdAsync(Guid subjectId);
        Task<bool> IsUserActiveAsync(Guid subjectId);
        Task AddUserAsync(User user);
        Task AddUserLoginAsync(Guid subjectId, string loginProvider, string providerKey);
        Task AddUserClaimAsync(Guid subjectId, string claimType, string claimValue);
    }

    public class UsersService : IUsersService
    {
        private readonly IUnitOfWork _uow;
        private readonly DbSet<User> _users;

        public UsersService(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _users = _uow.Set<User>();
        }

        public async Task<bool> AreUserCredentialsValidAsync(string username, string password)
        {
            // get the user
            var user = await GetUserByUsernameAsync(username);
            if (user == null)
            {
                return false;
            }

            return user.HashedPassword == password.GetSha256Hash();
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            return _users.FirstOrDefaultAsync(u =>
                u.UserClaims.Any(c => c.ClaimType == "email" && c.ClaimValue == email));
        }

        public Task<User> GetUserByProviderAsync(string loginProvider, string providerKey)
        {
            return _users
                .FirstOrDefaultAsync(u =>
                    u.UserLogins.Any(l => l.LoginProvider == loginProvider && l.ProviderKey == providerKey));
        }

        public Task<User> GetUserByIdAsync(Guid subjectId)
        {
            return _users.FirstOrDefaultAsync(u => u.Id == subjectId);
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            return _users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<IEnumerable<UserClaim>> GetUserClaimsBySubjectIdAsync(Guid subjectId)
        {
            var user = await _users.Include(x => x.UserClaims).FirstOrDefaultAsync(u => u.Id == subjectId);
            return user == null ? new List<UserClaim>() : user.UserClaims.ToList();
        }

        public async Task<IEnumerable<UserLogin>> GetUserLoginsBySubjectIdAsync(Guid subjectId)
        {
            var user = await _users.Include(x => x.UserLogins).FirstOrDefaultAsync(u => u.Id == subjectId);
            return user == null ? new List<UserLogin>() : user.UserLogins.ToList();
        }

        public async Task<bool> IsUserActiveAsync(Guid subjectId)
        {
            var user = await GetUserByIdAsync(subjectId);
            if (user == null)
            {
                throw new ArgumentException("User with given subjectId not found.", subjectId.ToString());
            }

            return user.IsActive;
        }

        public async Task AddUserAsync(User user)
        {
            _users.Add(user);
            await _uow.SaveChangesAsync();
        }

        public async Task AddUserLoginAsync(Guid subjectId, string loginProvider, string providerKey)
        {
            var rnd = new Random();
            var id = rnd.Next(1, 99999);
            var user = await GetUserByIdAsync(subjectId);
            if (user == null)
            {
                throw new ArgumentException("User with given subjectId not found.", subjectId.ToString());
            }
            user.AddLogin(new UserLogin(loginProvider, providerKey));
            await _uow.SaveChangesAsync();
        }

        public async Task AddUserClaimAsync(Guid subjectId, string claimType, string claimValue)
        {
            var user = await GetUserByIdAsync(subjectId);
            if (user == null)
            {
                throw new ArgumentException("User with given subjectId not found.", subjectId.ToString());
            }
            user.AddClaim(new UserClaim(claimType, claimValue));
            await _uow.SaveChangesAsync();
        }
    }
}
