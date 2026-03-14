using FoodDelivery.Application.Common.Exceptions;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Account.Commands.Logout
{
    public class LogoutCommandHandler (IUserService userService)   : IRequestHandler<LogoutCommand, Unit>
    {
        public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var user = await userService.GetByIdAsync(request.UserId)??throw new UnauthorizedException("Account.NotFound","User not found.");

            user.RevokeRefreshToken();
          await   userService.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
