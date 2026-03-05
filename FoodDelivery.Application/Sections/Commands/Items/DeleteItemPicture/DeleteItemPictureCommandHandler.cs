using FoodDelivery.Application.Common.Exceptions;
using FoodDelivery.Application.Common.Interfaces;
using FoodDelivery.Application.Common.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Menus.Commands.Items.DeleteItemPicture
{
    public sealed class DeleteItemPictureCommandHandler(IMenuRepository menuRepository,IImageStorageService imageStorageService) : IRequestHandler<DeleteItemPictureCommand>
    {
        public async Task Handle(DeleteItemPictureCommand request, CancellationToken cancellationToken)
        {
            var menu = await menuRepository.GetByRestaurantIdAsync(request.RestaurantId, cancellationToken)
                   ?? throw new NotFoundException("Menu.NotFound", "Menu not found.");

            var section = menu.GetSection(request.SectionId)
                          ?? throw new NotFoundException("Section.NotFound", "Section not found.");

            var item = section.GetItem(request.ItemId)
                       ?? throw new NotFoundException("Item.NotFound", "Item not found.");
            item.RemovePicture(request.Url);
            await imageStorageService.DeleteAsync(request.Url, "ItemPictures" , cancellationToken);

            await menuRepository.SaveChangesAsync(cancellationToken); 

        }
    }
}
