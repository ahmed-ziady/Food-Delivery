using FoodDelivery.Contracts.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        [HttpGet]
        public ActionResult<AccountResponse> GetAccount()
        {
            var response = new AccountResponse(
    "afsdkf;a",
    "Test",
    "Test",
    "test@gmail.com",
    "+201200000000",
    "Test Bio",
    "https://example.com/profile.jpg",
    false,
    false
);

            return Ok(response);
        }
    }
}
