using FoodDelivery.Application.Account.Common;
using FoodDelivery.Application.Common.Exceptions;
using FoodDelivery.Application.Common.Interfaces;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Application.Common.Mapping;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Account.Commands.UpdateProfileImage
{
    public sealed class UploadProfileImageHandler(IUserRepository userRepository, IImageStorageService imageStorageService) : IRequestHandler<UploadProfileImageCommand, AccountResult>
    {
        public async Task<AccountResult> Handle(UploadProfileImageCommand request, CancellationToken cancellationToken)
        {
           var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken) ?? throw new NotFoundException(
                "Account.NotFound",
                "Account not found.");

            var newImageUrl = await imageStorageService.UploadAsync(request.File, "profile", cancellationToken);
            try
            {
                if (!string.IsNullOrWhiteSpace(user.ProfilePictureUrl))
                {
                    await imageStorageService.DeleteAsync(
                        user.ProfilePictureUrl,
                        "profile",
                        cancellationToken);
                }

                user.UpdateProfilePicture(newImageUrl);
                
                await userRepository.SaveChangesAsync(cancellationToken);

            }
            catch
            {
                await imageStorageService.DeleteAsync(
                    newImageUrl,
                    "profile",
                    cancellationToken);

                throw;
            }

            return user.ToAccountResult();
        }
    }
}
