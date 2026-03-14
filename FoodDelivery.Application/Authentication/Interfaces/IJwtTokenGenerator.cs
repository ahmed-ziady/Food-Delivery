using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Authentication.Interfaces;

public interface IJwtTokenGenerator
{
   Task<  string> GenerateAccessToken(User user);

    string GenerateRefreshTokenValue();
}
