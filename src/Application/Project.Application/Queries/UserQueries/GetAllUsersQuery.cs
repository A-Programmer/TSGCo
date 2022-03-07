using System;
using MediatR;
using Project.Application.Dtos.UserDtos;
using Project.Domain.Shared.Utilities;

namespace Project.Application.Queries.UserQueries
{
    public class GetAllUsersQuery : IRequest<PaginatedList<UserDto>>
    {
        public GetAllUsersQuery(int? pageNumber, int? pageSize)
        {
            PageIndex = pageNumber ?? 1;
            PageSize = pageSize ?? 10;
        }


        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
    }
}
