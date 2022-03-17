using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.Auth.Contracts;
using Project.Auth.Domain;

namespace Project.Auth.Services
{
    public interface IUserClaimsService
    {
        Task<UserClaim> GetUserClaimAsync(Guid subjectId, string claimType);
        Task<List<UserClaim>> GetUserClaimsAsync(Guid subjectId, IList<string> claimTypes);
        Task AddOrUpdateUserClaimValueAsync(Guid subjectId, string claimType, string claimValue);
        Task AddOrUpdateUserClaimValuesAsync(
            Guid subjectId,
            IEnumerable<(string ClaimType, string ClaimValue)> userClaims);
    }

    public class UserClaimsService : IUserClaimsService
    {
        private readonly IUnitOfWork _uow;
        private readonly DbSet<UserClaim> _userClaims;

        public UserClaimsService(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _userClaims = _uow.Set<UserClaim>();
        }

        public Task<UserClaim> GetUserClaimAsync(Guid subjectId, string claimType)
        {
            return _userClaims.FirstOrDefaultAsync(userClaim =>
                userClaim.ClaimType == claimType && userClaim.UserId == subjectId);
        }

        public Task<List<UserClaim>> GetUserClaimsAsync(Guid subjectId, IList<string> claimTypes)
        {
            return _userClaims.Where(
                    userClaim => userClaim.UserId == subjectId && claimTypes.Contains(userClaim.ClaimType))
                .ToListAsync();
        }

        public async Task AddOrUpdateUserClaimValuesAsync(
            Guid subjectId,
            IEnumerable<(string ClaimType, string ClaimValue)> userClaims)
        {
            foreach (var userClaim in userClaims)
            {
                var dbRecord = await _userClaims.FirstOrDefaultAsync(dbClaim =>
                    dbClaim.ClaimType == userClaim.ClaimType &&
                    dbClaim.UserId == subjectId);
                if (dbRecord == null)
                {
                    var claim = new UserClaim(userClaim.ClaimType, userClaim.ClaimValue);
                    claim.SetUserId(subjectId);
                    _userClaims.Add(claim);
                }
                else
                {
                    dbRecord.UpdateClaimValue(userClaim.ClaimValue);
                }
            }

            await _uow.SaveChangesAsync();
        }

        public async Task AddOrUpdateUserClaimValueAsync(Guid subjectId, string claimType, string claimValue)
        {
            var dbRecord = await _userClaims.FirstOrDefaultAsync(dbClaim =>
                dbClaim.ClaimType == claimType &&
                dbClaim.UserId == subjectId);
            if (dbRecord == null)
            {
                var claim = new UserClaim(claimType, claimValue);
                claim.SetUserId(subjectId);
                _userClaims.Add(claim);
            }
            else
            {
                dbRecord.UpdateClaimValue(claimValue);
            }
            await _uow.SaveChangesAsync();
        }
    }
}