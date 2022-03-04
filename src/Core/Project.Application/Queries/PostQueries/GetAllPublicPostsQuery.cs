using System;
using MediatR;
using Project.Application.Dtos.PublicDtos.PostPublicDtos;
using Project.Common.Utilities;

namespace Project.Application.Queries.PostQueries
{
    public class GetAllPublicPostsQuery : IRequest<PaginatedList<PublicPostsListDto>>
    {
        public GetAllPublicPostsQuery(int? pageNumber, int? pageSize, bool includeViews = false, bool includeVotes = false,
                bool includeComments = false)
        {
            PageIndex = pageNumber ?? 1;
            PageSize = pageSize ?? 10;
            IncludeComments = includeComments;
            IncludeViews = includeViews;
            IncludeVotes = includeVotes;
        }


        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public bool IncludeComments { get; private set; }
        public bool IncludeViews { get; private set; }
        public bool IncludeVotes { get; private set; }
    }
}
