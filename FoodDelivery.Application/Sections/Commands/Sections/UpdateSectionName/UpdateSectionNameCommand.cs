using FoodDelivery.Application.Sections.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Menus.Commands.Sections.UpdateSectionName
{
    public sealed record UpdateSectionNameCommand (Guid OwenerId, Guid SectionId, string Name):IRequest<MenuSectionDto>;
    
}
