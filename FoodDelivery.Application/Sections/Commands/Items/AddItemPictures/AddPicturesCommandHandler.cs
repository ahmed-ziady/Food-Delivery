using FoodDelivery.Application.Common.Exceptions;
using FoodDelivery.Application.Common.Interfaces;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Application.Menus.Commands.Items.AddItemPictures;
using FoodDelivery.Domain.Common.Exceptions;
using FoodDelivery.Domain.Commons.Exceptions;
using FoodDelivery.Domain.ValueObjects;
using MediatR;

namespace FoodDelivery.Application.Sections.Commands.Items.AddItemPictures;

public sealed class AddPicturesCommandHandler(
    IMenuRepository menuRepository,
    IImageStorageService imageStorageService)
    : IRequestHandler<AddItemPicturesCommand>
{
    public async Task Handle(AddItemPicturesCommand request, CancellationToken cancellationToken)
    {
        var menu = await menuRepository.GetByRestaurantIdAsync(
            request.RestaurantId,
            cancellationToken)
            ?? throw new NotFoundException("Menu.NotFound", "Menu not found.");

        var section = menu.GetSection(request.SectionId)
            ?? throw new NotFoundException("Section.NotFound", "Section not found.");

        var item = section.GetItem(request.ItemId)
            ?? throw new NotFoundException("Item.NotFound", "Item not found.");

        if (item.Pictures.Count + request.Pictures.Count() > 5)
            throw new TooManyImagesException(5);

        var pictures = new List<Picture>();

        foreach (var file in request.Pictures)
        {
            var url = await imageStorageService
                .UploadAsync(file, "ItemPictures", cancellationToken);

            pictures.Add(new Picture(url));
        }

        item.AddPictures(pictures);

        await menuRepository.SaveChangesAsync(cancellationToken);
    }
}