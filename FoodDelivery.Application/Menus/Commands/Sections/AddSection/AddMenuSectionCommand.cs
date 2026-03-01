using FoodDelivery.Application.Menus.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Menus.Commands.Sections.AddSection
{
   public record AddMenuSectionCommand(Guid OwenerId, string Name):IRequest<MenuSectionDto>;
    
}
