using Microsoft.AspNetCore.Mvc;

namespace FitCommunity.User.Service.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("All Users");
        }
    }
}
