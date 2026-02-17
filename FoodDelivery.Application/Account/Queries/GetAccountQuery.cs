using FoodDelivery.Application.Account.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Account.Queries
{
 public record GetAccountQuery (Guid UserId) : IRequest<AccountResult>;

}
