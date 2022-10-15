﻿using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Project.Application.Commands.RoleCommands;
using Project.Application.Dtos.RoleDtos;
using Project.Common;
using Project.Common.Exceptions;
using Project.Domain;

namespace Project.Application.Handlers.RoleHandlers
{
    public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, DeleteRoleResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DeleteRoleResponse> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {

            var role = await _unitOfWork.Roles.GetByIdAsync(request.Id);

            var usersRole = await _unitOfWork.Roles.GetUsersByRoleId(request.Id);

            if (role == null)
                throw new AppException(ApiResultStatusCode.NotFound, "نقش مورد نظر یافت نشد.", HttpStatusCode.NotFound);

            if (usersRole.Any())
                throw new AppException(ApiResultStatusCode.ServerError, "نقش مود نظر حاوی کاربر می باشد و نمی تواند حذف شود.", HttpStatusCode.InternalServerError);

            _unitOfWork.Roles.Remove(role);
            await _unitOfWork.CommitAsync();

            return new DeleteRoleResponse(role.Id);
        }
    }
}
