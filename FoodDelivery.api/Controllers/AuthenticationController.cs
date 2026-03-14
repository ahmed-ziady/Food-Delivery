using FoodDelivery.Application.Authentication.Commands.FacebookLogin;
using FoodDelivery.Application.Authentication.Commands.ForgotPassword;
using FoodDelivery.Application.Authentication.Commands.GoogleLogin;
using FoodDelivery.Application.Authentication.Commands.Login;
using FoodDelivery.Application.Authentication.Commands.Refresh;
using FoodDelivery.Application.Authentication.Commands.Register;
using FoodDelivery.Application.Authentication.Commands.ResendVerificationCode;
using FoodDelivery.Application.Authentication.Commands.ResetPassword;
using FoodDelivery.Application.Authentication.Commands.VerifyOtp;
using FoodDelivery.Contracts.Authentication;
using FoodDelivery.Contracts.RefreshToken;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace FoodDelivery.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(ISender _demdiator, IMapper _mapper) : ControllerBase
    {
        [EnableRateLimiting("register-limit")]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            await _demdiator.Send(command);
            return Accepted(new { Message = "Registration successful. Please check your email to verify your account." });
        }

        [EnableRateLimiting("register-limit")]
        [HttpPost("resend-v-code")]
        public async Task<IActionResult> ResendVerificationCode( ResendVerificatonCodeRequest request)
        {
            var command = _mapper.Map<ResendVerificationCodeCommand>(request);
            await _demdiator.Send(command);
            return Accepted(new {  Message = "If the email exists, a verification code has been sent." });
        }

        [EnableRateLimiting("login-limit")]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var command = _mapper.Map<LoginCommand>(request);
            var response = await _demdiator.Send(command);
            var authResponse = _mapper.Map<AuthenticationResponse>(response);
            return Ok(authResponse);

        }

        [EnableRateLimiting("user-limit")]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenRequest request)
        {
            var command = _mapper.Map<RefreshCommand>(request);
            var response = await _demdiator.Send(command);
            var authResponse = _mapper.Map<AuthenticationResponse>(response);
            return Ok(authResponse);
        }

        [EnableRateLimiting("user-limit")]
        [HttpPost("verifyEmail")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailRequest request)
        {
            var command = _mapper.Map<VerifyOtpCommand>(request);
            var response = await _demdiator.Send(command);
            var authResponse = _mapper.Map<AuthenticationResponse>(response);

            return Ok(authResponse);

        }

        [EnableRateLimiting("login-limit")]
        [HttpPost("external/google")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginRequest request)
        {
            var command = _mapper.Map<GoogleLoginCommand>(request);
            var response = await _demdiator.Send(command);
            var authResponse = _mapper.Map<AuthenticationResponse>(response);
            return Ok(authResponse);
        }

        [EnableRateLimiting("login-limit")]
        [HttpPost("external/facebook")]
        public async Task<IActionResult> FacebookLogin(FacebookLoginRequest request)
        {
            var command = _mapper.Map<FacebookLoginCommand>(request);
            var response = await _demdiator.Send(command);
            var authResponse = _mapper.Map<AuthenticationResponse>(response);
            return Ok(authResponse);
        }

        [EnableRateLimiting("register-limit")]
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordRequest request)
        {
            var command = _mapper.Map<ForgotPasswordCommand>(request);
            await _demdiator.Send(command);
            return Accepted(new { Message = "Please check your email to verify your email." });
        }

        [EnableRateLimiting("register-limit")]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var command = _mapper.Map<ResetPasswordCommand>(request);
            await _demdiator.Send(command);
            return Accepted(new { Message = "Password reset successful. You can now log in with your new password." });


        }

    }
}
