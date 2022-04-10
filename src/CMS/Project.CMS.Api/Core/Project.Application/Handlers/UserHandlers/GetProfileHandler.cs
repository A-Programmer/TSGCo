using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Application.Dtos.UserDtos;
using Project.Application.Queries.UserQueries;
using Project.Common;
using Project.Common.Exceptions;
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
                throw new AppException(ApiResultStatusCode.NotFound, "کاربر یافت نشد", HttpStatusCode.NotFound);

            var roles = user.Roles.Select(x => x.Name).ToArray();
            var profileDto = new GetUserProfileDto(user.Id, user.UserName, user.Email, user.PhoneNumber, user.Profile?.FirstName, user.Profile?.LastName, user.Profile?.FullName,
                user.Profile?.AboutMe, roles, user.Profile?.ProfileImageUrl, user.Profile?.BirthDate);

            return profileDto;
        }
    }
}
