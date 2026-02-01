using FoodDelivery.Application.Common.Interfaces.Authentication;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Application.Services.Authentication.Common;
using FoodDelivery.Domain.Entities;
using MediatR;

namespace FoodDelivery.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository) : IRequestHandler<RegisterCommand, AuthenticationResult>
    {
        public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken)

        {
            await Task.CompletedTask;
            // 🟥 409 – Conflict (email already exists)
            if (userRepository.GetUserByEmail(command.Email) is not null)
            {
                throw new InvalidOperationException("Email already exists.");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                Password = command.Password, // later: hash it
                PhoneNumber = command.PhoneNumber
            };

            userRepository.Add(user);

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

