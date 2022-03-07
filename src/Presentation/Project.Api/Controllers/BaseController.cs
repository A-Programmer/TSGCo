using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Domain.Shared;
using Project.WebFrameworks.Api;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Api.Controllers
{
    [ApiController]
    [Route(Routes.BaseRootAddress)]
    public class BaseController : ControllerBase
    {
        [NonAction]
        protected ActionResult CustomPagedOk(object data, int? pageIndex, int? totalPages, int? totalItems,
            bool? showPagination, string message = "")
        {
            return Ok(new ResultMessage<object>(true, data, Status.Success, message, pageIndex, totalPages, totalItems, showPagination));
        }

        [NonAction]
        protected ActionResult CustomOk(object data, string message = "")
        {
            return Ok(new ResultMessage<object>(true, data, Status.Success, message));
        }

        [NonAction]
        protected ActionResult CustomError(Status status, string message = "")
        {
            return BadRequest(new ResultMessage(false, status, message));
        }
    }
}
