using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Application.Dtos.UserDtos;
using Project.Application.Queries.UserQueries;
using Project.Domain;

namespace Project.Application.Handlers.UserHandlers
{
    public class GetProfileHandler : IRequestHandler<GetProfileQuery, GetUserProfileDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProfileHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetUserProfileDto> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetUserProfile(request.Id);

            if (user == null)
                return null;

            var userRoles = (await _unitOfWork.Roles.GetRolesByIdsAsync(user.Roles.Select(x => x.RoleId).ToArray())).Select(x => x.Name).ToArray();
            var profileDto = new GetUserProfileDto(user.Id, user.UserName, user.Email, user.PhoneNumber, user.Profile?.FirstName, user.Profile?.LastName, user.Profile?.FullName,
                user.Profile?.AboutMe, userRoles, user.Profile?.ProfileImageUrl, user.Profile?.BirthDate);

            return profileDto;
        }
    }
}
