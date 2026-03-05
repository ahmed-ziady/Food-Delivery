using FluentValidation;
using FoodDelivery.Application.Sections.Common;
using FoodDelivery.Domain.Enums;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Menus.Commands.Items.AddItem
{
    public sealed record AddItemCommand(Guid RestaurantId, Guid SectionId,String Name, string? Description,decimal Price,DeliveryType DeliveryType) :IRequest<MenuSectionDto>;
    
}
