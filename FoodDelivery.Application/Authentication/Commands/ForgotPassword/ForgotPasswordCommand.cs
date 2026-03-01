using MediatR;

namespace FoodDelivery.Application.Authentication.Commands.ForgotPassword
{
    public sealed record ForgotPasswordCommand(string Email) : IRequest<Unit>;

}
