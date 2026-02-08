using FoodDelivery.Application.Services.Authentication.Common;
using MediatR;

namespace FoodDelivery.Application.Authentication.Commands.Refresh
{
    public record RefreshCommand(
        string RefreshToken
    ) : IRequest<AuthenticationResult>;
}
