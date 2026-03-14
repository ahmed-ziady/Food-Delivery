using FoodDelivery.Application.Account.Commands.Address.Commands.DeleteAddress;
using FoodDelivery.Application.Account.Commands.Address.Commands.SetAddress;
using FoodDelivery.Application.Account.Commands.Address.Commands.UpdateAddress;
using FoodDelivery.Application.Account.Commands.Address.Queries;
using FoodDelivery.Application.Account.Commands.ChangeEmail.ChangeEmailConfirm;
using FoodDelivery.Application.Account.Commands.ChangeEmail.ChangeEmailRequest;
using FoodDelivery.Application.Account.Commands.Logout;
using FoodDelivery.Application.Account.Commands.UpdateProfile;
using FoodDelivery.Application.Account.Commands.UpdateProfileImage;
using FoodDelivery.Application.Account.Queries;
using FoodDelivery.Contracts.Account;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FoodDelivery.api.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]

    public class AccountController(ISender mediator, IMapper mapper) : ControllerBase
    {
        [NonAction]
        private Guid GetUserId()
        {
            var userIdClaim = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            return userIdClaim switch
            {
                null => throw new UnauthorizedAccessException("User ID claim not found."),
                _ => Guid.Parse(userIdClaim.Value)
            };
        }

        [HttpGet]
        public async Task<ActionResult<AccountResponse>> GetAccount()
        {
            var userId = GetUserId();

            var query = new GetAccountQuery(userId);

            var result = await mediator.Send(query);

            return Ok(result);
        }

        [HttpPatch]
        public async Task<ActionResult<AccountResponse>> UpdateProfile(UpdateProfileRequest request)
        {
            var userId = GetUserId();
            var command = mapper.Map<UpdateProfileCommand>((request, userId));
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPatch("upload-profile-image")]
        public async Task<ActionResult<AccountResponse>> UploadProfileImage(IFormFile file)
        {
            var userId = GetUserId();
            var command = new UploadProfileImageCommand(userId, file);
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userId = GetUserId();
            var command = new LogoutCommand(userId);
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPost("change-email")]
        public async Task<IActionResult> ChangeEmailAsync(string newEmail)
        {
            var userId = GetUserId();
            var command = new ChangeEmailCommand(userId, newEmail);
            await mediator.Send(command);
            return Accepted(new { Message = "Please check your New email to verify your email." });
        }

        [HttpPost("confirm-email-change")]
        public async Task<IActionResult> ConfirmEmailChange(ConfirmChangeEmailRequest request)
        {
            var userId = GetUserId();
            var command = mapper.Map<ConfirmChangeEmailCommand>((request, userId));
            var result = await mediator.Send(command);
            var response = mapper.Map<AccountResponse>(result);
            return Ok(response);

        }
        [HttpPost("address")]
        public async Task<IActionResult> SetAddress(SetAddressRequest request)
        {
            var userId = GetUserId();
            var command = mapper.Map<SetAddressCommand>((request, userId));
            var result = await mediator.Send(command);
            var response = mapper.Map<AddressResponse>(result);
            return Ok(response);
        }
        [HttpGet("addresses")]
        public async Task<IActionResult> GetAddressesAsync()
        {
            var userId = GetUserId();
            var query = new GetAddressesQuery(userId);
            var result = await mediator.Send(query);
            return Ok(result);
        }
        [HttpPut("address")]
        public async Task<IActionResult> UpdateAddress(UpdateAddressRequest request)
        {
            var command = mapper.Map<UpdateAddressCommand>(request);
            var result = await mediator.Send(command);
            return Ok(result);
        }
        [HttpDelete("address/{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {

            var command = new DeleteAddressCommand(id);
            await mediator.Send(command);
            return NoContent();
        }

    }
}
