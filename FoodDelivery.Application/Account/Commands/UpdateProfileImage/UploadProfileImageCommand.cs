using FoodDelivery.Application.Account.Common;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace FoodDelivery.Application.Account.Commands.UpdateProfileImage
{
    public sealed record UploadProfileImageCommand(
        Guid UserId,
        IFormFile File
    ) : IRequest<AccountResult>;
}
