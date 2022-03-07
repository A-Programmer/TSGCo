using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Application.Commands.UserCommands;
using Project.Domain.Shared;
using Project.Domain.Shared.Exceptions;
using Project.Domain;
using Project.Domain.Models.UserEntities;

namespace Project.Application.Handlers.UserHandlers
{
    public class UpdateProfileHandler : IRequestHandler<UpdateProfileCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProfileHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetUserByIdAndUserName(request.Id, request.UserName);

            if (user == null)
                throw new AppException(ApiResultStatusCode.NotFound, "کاربر مورد نظر یافت نشد", HttpStatusCode.NotFound);

            user.Update(request.Email, request.PhoneNumber);

            if (user.Profile != null)
            {
                user.Profile.Update(request.FirstName, request.LastName, request.ProfileImageUrl, request.AboutMe, request.BirthDate);
            }
            else
            {
                var profile = new UserProfile(request.FirstName, request.LastName, request.ProfileImageUrl, request.AboutMe, request.BirthDate);
                profile.SetUserId(user.Id);
                user.UpdateProfile(profile);
            }

            await _unitOfWork.CommitAsync();

            return user.Id;
        }
    }
}
