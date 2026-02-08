using FoodDelivery.Application.Common.Interfaces.Authentication;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Application.Services.Authentication.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Authentication.Commands.Refresh
{
    internal class RefreshCommandHandler(IRefreshTokenRepository refreshToken, IJwtTokenGenerator jwtTokenGenerator,IUserRepository userRepository) : IRequestHandler<RefreshCommand, AuthenticationResult>
    {


        public async Task<AuthenticationResult> Handle(
            RefreshCommand request,
            CancellationToken cancellationToken)
        {
            var storedToken = await refreshToken.GetByTokenAsync(request.RefreshToken);

            if (storedToken is null ||
                storedToken.IsRevoked ||
                storedToken.ExpiresAt <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException();
            }

            // revoke old token
            storedToken.IsRevoked = true;
            await refreshToken.UpdateAsync(storedToken);

            // get user safely
            var user =  userRepository.GetUserById(storedToken.UserId)
                ?? throw new UnauthorizedAccessException();

            var accessToken =
                jwtTokenGenerator.GenerateAccessToken(user);

            var newRefreshToken =
                jwtTokenGenerator.GenerateRefreshToken(user.Id);

            await refreshToken.AddAsync(newRefreshToken);

            return new AuthenticationResult(
                user,
                accessToken,
                newRefreshToken.Token
            );
        }
    }
}
