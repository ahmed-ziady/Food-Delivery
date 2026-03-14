using FoodDelivery.Application.Account.Common;
using FoodDelivery.Application.Common.Exceptions;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Application.Common.Mapping;
using FoodDelivery.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;

namespace FoodDelivery.Application.Account.Commands.UpdateProfile
{
    public sealed class UpdateProfileHandler(
        IUserService userService)
        : IRequestHandler<UpdateProfileCommand, AccountResult>
    {
        public async Task<AccountResult> Handle(
            UpdateProfileCommand request,
            CancellationToken cancellationToken)
        {
            var user = await userService
                .GetByIdAsync(request.UserId)
                ?? throw new NotFoundException(
                    "Account.NotFound",
                    "Account not found.");

                user.UpdateProfile(
                    request.FirstName,
                    request.LastName,
                    request.Bio);

            await userService.SaveChangesAsync(cancellationToken);

            return  user.ToAccountResult();
        }
    }
}