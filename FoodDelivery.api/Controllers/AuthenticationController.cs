using FoodDelivery.Application.Authentication.Commands.Login;
using FoodDelivery.Application.Authentication.Commands.Register;
using FoodDelivery.Contracts.Authentication;
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
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            var response = await _demdiator.Send(command);

            var authResponse = _mapper.Map<AuthenticationResponse>(response);
            return Ok(authResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var command = _mapper.Map<LoginCommand>(request);   
            var response = await _demdiator.Send(command);
            var authResponse = _mapper.Map<AuthenticationResponse>(response);
            return Ok(authResponse);

        }


    }
}
