using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Api.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)] 
    public class ErrorController : ControllerBase
    {
        [HttpGet("/error")]
        public IActionResult Error()
        {
            var exception =
                HttpContext.Features.Get<IExceptionHandlerPathFeature>()?.Error;

            // Problem() هيستخدم ProblemDetailsFactory تلقائيًا
            return Problem(
                detail: exception?.Message,
                title: "An unexpected error occurred"
            );
        }
    }
}
