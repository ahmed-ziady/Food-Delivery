using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TestController : ControllerBase
    {
        [Authorize]
        [HttpGet ("TestAuthorization")]
        public IActionResult Get()
        {
            return Ok("API is working!");
        }
    }
}
