using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Domain.Contracts.Services.NotificationServices;


namespace Project.Api.Controllers
{
    [ApiVersion("1")]
    public class TestsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly INotificationService _notificationService;

        public TestsController(IMediator mediator, INotificationService notificationService)
        {
            _mediator = mediator;
            _notificationService = notificationService;
        }

        [Authorize]
        [HttpGet("/api/tests/getdata")]
        public IActionResult GetData()
        {
            var data = new List<string>
            {
                "Kamran",
                "Mohsen",
                "Saeed"
            };
            return new JsonResult(data);
        }

    }
}
