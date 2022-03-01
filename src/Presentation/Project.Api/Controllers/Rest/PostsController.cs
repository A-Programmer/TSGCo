using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Api.ViewModels.PostViewModels;
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
            var query = new GetAllPublicPostsQuery();
            var posts = await _mediator.Send(query);

            var result = posts.Select(x => new Posts_VM(x.Id, x.Title, x.Description));

            return CustomOk(result);
        }

    }
}
