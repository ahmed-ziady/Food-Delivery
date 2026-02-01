using FoodDelivery.Application.Common.Interfaces.Authentication;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Application.Services.Authentication.Common;
using FoodDelivery.Domain.Entities;
using MediatR;

namespace FoodDelivery.Application.Authentication.Commands.Login
{
    public class LoginCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository) : IRequestHandler<LoginCommand, AuthenticationResult>
    {
        public async Task<AuthenticationResult> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            // 🟥 401 – Unauthorized (user not found)
            if (userRepository.GetUserByEmail(command.Email) is not User user)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            // 🟥 401 – Unauthorized (wrong password)
            if (user.Password != command.Password)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }

            var token = jwtTokenGenerator.GenerateToken(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email);

            return new AuthenticationResult(
               user,
                token);
        }
    }
}
