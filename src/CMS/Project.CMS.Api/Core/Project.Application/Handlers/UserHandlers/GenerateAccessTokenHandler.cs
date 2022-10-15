﻿using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Application.Commands.UserCommands;
using Project.Application.Dtos.UserDtos;
using Project.Application.Services.JwtServices;
using Project.Common;
using Project.Common.Exceptions;
using Project.Common.Utilities;
using Project.Domain;
using Project.Domain.Models.UserEntities;

namespace Project.Application.Handlers.UserHandlers
{
    public class GenerateAccessTokenHandler : IRequestHandler<GenerateAccessTokenCommand, AccessTokenDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GenerateAccessTokenHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AccessTokenDto> Handle(GenerateAccessTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Users.GetUserWithRolesByUserName(request.UserName);

            if (user == null)
                user = await _unitOfWork.Users.GetUserByEmail(request.UserName);

            if (user == null)
                user = await _unitOfWork.Users.GetUserByPhone(request.UserName);

            if (user == null)
                throw new AppException(ApiResultStatusCode.NotFound, "کاربر مورد نظر یافت نشد", HttpStatusCode.NotFound);

            var tokenServices = new TokenServices();

            var token = tokenServices.GenerateToken(user, request.Options);

            var refreshTokenValue = SecurityHelper.GenerateToken();

            var userToken = new UserToken(TokenTypes.AuthenticationToken, token.ToString(), DateTimeOffset.Now.AddMinutes(request.Options.ExpirationInMinutes));

            var refreshToken = new UserToken(TokenTypes.RefreshToken, refreshTokenValue, DateTimeOffset.Now.AddMinutes(request.Options.ExpirationInMinutes * 2));

            user.AddToken(userToken);
            user.AddToken(refreshToken);

            await _unitOfWork.CommitAsync();

            return new AccessTokenDto(token, refreshToken.Token);
        }
    }
}