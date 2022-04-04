
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.CMS.Services;

namespace Project.CMS.Controllers
{
    [Route("[controller]/[action]")]
    public class TestsController : Controller
    {
        private readonly ITokenServices _tokenServices;
        private readonly IHttpContextAccessor _ctx;
        public TestsController(ITokenServices tokenServices, IHttpContextAccessor ctx)
        {
            _tokenServices = tokenServices;
            _ctx = ctx;
        }
        public async Task<IActionResult> GetIdentity()
        {
            var client = await _tokenServices.GetHttpClientAsync("https://localhost:6001");
            var response = await client.GetAsync("WeatherForecast/Get2");
            if(!response.IsSuccessStatusCode)
                return new JsonResult(response.StatusCode);
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                return Ok(content);
            }
        }





        [Authorize]
        public async Task<IActionResult> Claims()
        {
            return View();
        }

    }
}