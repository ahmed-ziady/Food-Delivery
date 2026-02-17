using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Authentication.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(User user);

    string GenerateRefreshTokenValue();
}
