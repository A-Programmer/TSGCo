using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Application.Commands.UserCommands;
using Project.Application.Dtos.UserDtos;
using Project.Domain.Shared;
using Project.Domain.Shared.Exceptions;
using Project.Domain;
using Project.Domain.Models.UserEntities;

namespace Project.Application.Handlers.UserHandlers
{
    public class AddOrUpdateUserProfileHandler : IRequestHandler<AddOrUpdateUserProfileCommand, AddOrUpdateUserProfileResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddOrUpdateUserProfileHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AddOrUpdateUserProfileResponse> Handle(AddOrUpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetUserProfile(request.UserId);
            if (user == null)
                throw new AppException(ApiResultStatusCode.NotFound, "کاربر مورد نظر یافت نشد", System.Net.HttpStatusCode.NotFound);
            if (user.Profile == null)
                user.UpdateProfile(new UserProfile(request.FirstName, request.LastName, request.ProfileImageUrl, request.AboutMe, request.BirthDate));
            else
                user.Profile.Update(request.FirstName, request.LastName, request.ProfileImageUrl, request.AboutMe, request.BirthDate);

            await _unitOfWork.CommitAsync();

            return new AddOrUpdateUserProfileResponse(user.Id);
        }
    }
}
