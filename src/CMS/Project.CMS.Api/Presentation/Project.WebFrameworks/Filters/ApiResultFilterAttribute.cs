using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Project.WebFrameworks.Api;

namespace Project.WebFrameworks.Filters
{
    public class ApiResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult okObjectResult)
            {
                var apiResult = new ApiResult<object>(true, HttpStatusCode.OK, okObjectResult);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is OkResult okResult)
            {
                var apiResult = new ApiResult(true, HttpStatusCode.OK);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is BadRequestResult badRequestResult)
            {
                var apiResult = new ApiResult(false, HttpStatusCode.BadRequest);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is BadRequestObjectResult badRequestObjectResult)
            {
                var errorMessages = new List<string>();
                var message = badRequestObjectResult.Value.ToString();
                if (badRequestObjectResult.Value is SerializableError errors)
                {
                    errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct().ToList();
                    //message = string.Join(" | ", errorMessages);
                }
                var apiResult = new ApiResult(false, HttpStatusCode.BadRequest, errorMessages);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is ContentResult contentResult)
            {
                var apiResult = new ApiResult<object>(true, HttpStatusCode.OK, contentResult.Content);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is NotFoundResult notFoundResult)
            {
                var apiResult = new ApiResult(false, HttpStatusCode.NotFound);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is NotFoundObjectResult notFoundObjectResult)
            {
                var apiResult = new ApiResult<object>(false, HttpStatusCode.NotFound, notFoundObjectResult.Value);
                context.Result = new JsonResult(apiResult);
            }
            else if (context.Result is ObjectResult objectResult && objectResult.StatusCode == null
                && !(objectResult.Value is ApiResult))
            {
                var apiResult = new ApiResult<object>(true, HttpStatusCode.OK, objectResult.Value);
                context.Result = new JsonResult(apiResult);
            }

            base.OnResultExecuting(context);
        }
    }
}
