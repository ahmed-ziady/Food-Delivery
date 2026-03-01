using FluentValidation;
using FoodDelivery.Application.Menus.Common;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Menus.Commands.Items.AddItem
{
    public sealed record AddItemCommand(Guid RestaurantId, Guid SectionId,String Name, string? Description,decimal Price) :IRequest<MenuSectionDto>;
    
}
