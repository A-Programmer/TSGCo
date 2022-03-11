using IdentityServer4.Services;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Extensions;
using System.Security.Claims;

namespace Project.Auth.Services
{
    public class CustomUserProfileService : IProfileService
    {
        private readonly IUsersService _usersService;

        public CustomUserProfileService(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subjectId = context.Subject.GetSubjectId();
            var convertToGuidResult = Guid.TryParse(subjectId, out var guidSubjectId);
            if(!convertToGuidResult)
                throw new ArgumentException("Id must be a guid.");
            var claimsForUser = await _usersService.GetUserClaimsBySubjectIdAsync(guidSubjectId);
            context.IssuedClaims = claimsForUser.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList();
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var subjectId = context.Subject.GetSubjectId();
            var convertToGuidResult = Guid.TryParse(subjectId, out var guidSubjectId);
            if(!convertToGuidResult)
                throw new ArgumentException("Id must be a guid.");
            context.IsActive = await _usersService.IsUserActiveAsync(guidSubjectId);
        }
    }
}