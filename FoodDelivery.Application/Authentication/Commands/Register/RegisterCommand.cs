using FoodDelivery.Application.Services.Authentication.Common;
using MediatR;

namespace FoodDelivery.Application.Authentication.Commands.Register
{
    public record RegisterCommand(string FirstName,
        string LastName,
        string Email,
        string Password,
        string PhoneNumber) : IRequest<AuthenticationResult>;

}
