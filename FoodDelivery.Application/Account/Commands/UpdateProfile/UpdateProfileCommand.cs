using FoodDelivery.Application.Account.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Twilio.Rest.Api.V2010;

namespace FoodDelivery.Application.Account.Commands.UpdateProfile
{
    public record UpdateProfileCommand (
        Guid UserId,
        string? FirstName,
        string? LastName,
        string? Bio):IRequest<AccountResult>;

}
