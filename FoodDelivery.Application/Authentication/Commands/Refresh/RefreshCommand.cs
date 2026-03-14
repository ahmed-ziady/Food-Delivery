using FoodDelivery.Application.Common;
using MediatR;

namespace FoodDelivery.Application.Authentication.Commands.Refresh
{
    public record RefreshCommand(
        string RefreshToken
    ) : IRequest<AuthenticationResult>;
}
