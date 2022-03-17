
using Microsoft.AspNetCore.Mvc;
// using Project.Auth.Services;

namespace Project.Auth.Areas.Admin.ViewComponents
{
    public class UserProfileViewComponent : ViewComponent
    {
        // private readonly IUsersService _usersService;

        public UserProfileViewComponent()
            // IUsersService usersService)
        {
            // _usersService = usersService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // var user = await _usersService.GetUserByUsernameAsync("admin");
            
            // ViewData["UserName"] = user.UserName;
            // ViewData["ProfileImage"] = "/admin/img/user2-160x160.jpg";
            
            return View();
        }
    }
}