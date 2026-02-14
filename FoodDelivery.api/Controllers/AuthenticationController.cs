using FoodDelivery.Application.Authentication.Commands.FacebookLogin;
using FoodDelivery.Application.Authentication.Commands.GoogleLogin;
using FoodDelivery.Application.Authentication.Commands.Login;
using FoodDelivery.Application.Authentication.Commands.Refresh;
using FoodDelivery.Application.Authentication.Commands.Register;
using FoodDelivery.Application.Authentication.Commands.VerifyOtp;
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
            await _demdiator.Send(command);

            return Accepted(new
            {

                Message = "Registration successful. Please check your email to verify your account."

            });
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
        public async Task<IActionResult> Refresh(RefreshTokenRequest request)
        {
            var command = _mapper.Map<RefreshCommand>(request);
            var response = await _demdiator.Send(command);
            var authResponse = _mapper.Map<AuthenticationResponse>(response);
            return Ok(authResponse);
        }
        [HttpPost("verifyEmail")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailRequest request)
        {
            var command = _mapper.Map<VerifyOtpCommand>(request);
            var response = await _demdiator.Send(command);
            var authResponse = _mapper.Map<AuthenticationResponse>(response);

            return Ok(authResponse);

        }

        [HttpPost("external/google")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginRequest request)
        {
            var command = _mapper.Map<GoogleLoginCommand>(request);
            var response = await _demdiator.Send(command);
            var authResponse = _mapper.Map<AuthenticationResponse>(response);
            return Ok(authResponse);
        }
        [HttpPost("external/facebook")]
        public async Task<IActionResult> FacebookLogin(FacebookLoginRequest request)
        {
            var command = _mapper.Map<FacebookLoginCommand>(request);
            var response = await _demdiator.Send(command);
            var authResponse = _mapper.Map<AuthenticationResponse>(response);
            return Ok(authResponse);
        }
    }
}
