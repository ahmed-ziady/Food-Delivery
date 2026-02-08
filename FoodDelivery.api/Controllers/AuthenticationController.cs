using FoodDelivery.Application.Authentication.Commands.Login;
using FoodDelivery.Application.Authentication.Commands.Refresh;
using FoodDelivery.Application.Authentication.Commands.Register;
using FoodDelivery.Contracts.Authentication;
using FoodDelivery.Contracts.RefreshToken;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(ISender _demdiator, IMapper _mapper) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(Contracts.Authentication.RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            var response = await _demdiator.Send(command);

            var authResponse = _mapper.Map<AuthenticationResponse>(response);
            return Ok(authResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Contracts.Authentication.LoginRequest request)
        {
            var command = _mapper.Map<LoginCommand>(request);
            var response = await _demdiator.Send(command);
            var authResponse = _mapper.Map<AuthenticationResponse>(response);
            return Ok(authResponse);

        }
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromHeader] RefreshTokenRequest request)
        {
            var command = new RefreshCommand(request.RefreshToken);
            var response = await _demdiator.Send(command);
            var authResponse = _mapper.Map<AuthenticationResponse>(response);
            return Ok(authResponse);
        }


    }
}
