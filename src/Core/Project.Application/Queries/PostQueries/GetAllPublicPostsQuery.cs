using System;
using MediatR;
using Project.Application.Dtos.PublicDtos.PostPublicDtos;
using Project.Common.Utilities;

namespace Project.Application.Queries.PostQueries
{
    public class GetAllPublicPostsQuery : IRequest<PaginatedList<PublicPostsListDto>>
    {
        public GetAllPublicPostsQuery(int? pageNumber, int? pageSize)
        {
            PageIndex = pageNumber ?? 1;
            PageSize = pageSize ?? 10;
        }


        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
    }
}
