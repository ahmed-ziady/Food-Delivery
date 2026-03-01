//using FoodDelivery.Application.Account.Common;
//using FoodDelivery.Application.Common;
//using FoodDelivery.Application.Common.Exceptions;
//using FoodDelivery.Domain.Entities;
//using MediatR;
//using Microsoft.AspNetCore.Identity;

//namespace FoodDelivery.Application.Account.Commands.ChangeEmail.ChangeEmailConfirm
//{
//    public sealed class ChangeEmailConfirmCommandHandler(UserManager<User> userManager)
//            : IRequestHandler<ChangeEmailConfirmCommand, AccountResult>
//    {
//        public async Task<AccountResult> Handle(
//            ChangeEmailConfirmCommand request,
//            CancellationToken cancellationToken)
//        {
//            var user = await userManager.FindByIdAsync(request.UserId.ToString())
//                       ?? throw new UnauthorizedException(
//                           "User.NotFound",
//                           "User not found.");
//            var sameEmailUser = await userManager.FindByEmailAsync(request.NewEmail);
//            if (sameEmailUser != null && sameEmailUser.Id != user.Id)
//            {
//                throw new UnauthorizedException(
//                    "EmailAlreadyInUse",
//                    "The provided email is already in use by another account.");
//            }
//            var isValid = await userManager.VerifyUserTokenAsync(
//                user,
//                TokenOptions.DefaultEmailProvider,
//                "ChangeEmail"
//               );

//            if (!isValid)
//            {
//                throw new UnauthorizedException(
//                    "InvalidOtp",
//                    "Invalid or expired verification code.");
//            }

//            user.Email = request.NewEmail;
//            user.EmailConfirmed = true;

//            await userManager.UpdateAsync(user);

//            user.RevokeRefreshToken();
//            await userManager.UpdateAsync(user);

//            return user.ToAccountResult();
//        }

//    }
//}