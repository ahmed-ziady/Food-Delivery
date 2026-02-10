using FoodDelivery.Domain.UserAggregate;

namespace FoodDelivery.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(User user);

    string GenerateRefreshTokenValue();
}
