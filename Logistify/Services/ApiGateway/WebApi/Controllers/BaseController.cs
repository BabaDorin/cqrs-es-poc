using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class BaseController : ControllerBase
    {
        // Simulate authentication
        public string CurrentUserId = "current-user-id-placeholder";
    }
}
