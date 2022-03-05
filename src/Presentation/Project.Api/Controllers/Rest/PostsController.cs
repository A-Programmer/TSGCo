using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Api.ViewModels.PostViewModels;
using Project.Application.Queries.PostQueries;
using Project.WebFrameworks.Api;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Api.Controllers
{
    [ApiVersion("1")]
    public class PostsController : BaseController
    {
        private readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet(Routes.Posts.Get.GetAll)]
        public async Task<ActionResult<List<Posts_VM>>> Get()
        {
            var query = new GetAllPublicPostsQuery(0, 10, false, false, true);
            var posts = await _mediator.Send(query);

            var result = posts.Select(x => new Posts_VM(x.Id, x.Title, x.Slug, x.Description,
                x.ImageUrl, x.Status, x.CreatedAt, x.ModifiedAt, x.AuthorName, x.VotesCount, x.ViewsCount,
                x.CommentsCount, x.Categories.ToList(), x.Keywords.ToList()));

            return CustomOk(result);
        }

    }
}
