using MediatR;
using Microsoft.AspNetCore.Http;

namespace FoodDelivery.Application.Menus.Commands.Items.AddItemPictures
{
    public sealed record AddItemPicturesCommand(Guid RestaurantId, Guid SectionId, Guid ItemId, IEnumerable<IFormFile> Pictures) : IRequest;
}
