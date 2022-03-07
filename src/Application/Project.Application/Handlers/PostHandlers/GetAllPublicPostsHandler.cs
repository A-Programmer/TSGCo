using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Project.Application.Dtos.PublicDtos.PostPublicDtos;
using Project.Application.Queries.PostQueries;
using Project.Domain.Shared.Utilities;
using Project.Domain.Specifications.PostCategorySpecifications;
using Project.Domain.Specifications.PostKeywordSpecifications;
using Project.Domain.Specifications.PostSpecifications;

namespace Project.Application.Handlers.PostHandlers
{
    public class GetAllPublicPostsHandler : IRequestHandler<GetAllPublicPostsQuery, PaginatedList<PublicPostsListDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllPublicPostsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PaginatedList<PublicPostsListDto>> Handle(GetAllPublicPostsQuery request,
            CancellationToken cancellationToken)
        {
            var publicPostsSpecification = new PublicPostSpecification();
            var publicPosts = await _unitOfWork.Posts.GetAllAsync(request.IncludeComments,
                request.IncludeViews, request.IncludeVotes, publicPostsSpecification);

            var postsDto = new List<PublicPostsListDto>();
            foreach(var publicPost in publicPosts)
            {
                //Get Categories Names
                var getCategoriesByIdsSpecification = new GetCategoriesByIdsSpecification(
                    publicPost.Categories.Select(x => x.CategoryId).ToArray());
                var categories = (await _unitOfWork.Categories.GetAllAsync(getCategoriesByIdsSpecification))
                    .Select(x => x.Title).ToArray();
                //Get Author Full Name
                var user = await _unitOfWork.Users.GetByIdAsync(publicPost.UserId);
                var authorName = (user.Profile != null) ? user.Profile.FullName : user.UserName;

                //Get Keyword Names
                var getKeywordsByIdsSpecification = new GetKeywordsByIdsSpecification(
                    publicPost.Keywords.Select(x => x.KeywordId).ToArray());
                var keywords = (await _unitOfWork.Categories.GetAllAsync(getCategoriesByIdsSpecification))
                    .Select(x => x.Title).ToArray();

                postsDto.Add(
                    new PublicPostsListDto(publicPost.Id, publicPost.Title, publicPost.Slug, publicPost.Description,
                    publicPost.ImageUrl, publicPost.Status, publicPost.Votes.Count, publicPost.Views.Count, publicPost.Comments.Count(),
                    authorName, categories, keywords, publicPost.ShowInSlides, publicPost.CreatedAt, publicPost.ModifiedAt)
                );
            }

            return PaginatedList<PublicPostsListDto>.Create(postsDto.AsQueryable(), request.PageIndex, request.PageSize);
        }
    }
}
