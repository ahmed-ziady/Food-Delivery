using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Menus.Commands.Items.DeleteItemPicture
{
    public sealed record DeleteItemPictureCommand(Guid RestaurantId, Guid SectionId, Guid ItemId, string Url):IRequest;
    
}
