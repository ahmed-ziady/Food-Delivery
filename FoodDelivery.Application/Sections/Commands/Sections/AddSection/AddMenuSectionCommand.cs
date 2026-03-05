using FoodDelivery.Application.Sections.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Sections.Commands.Sections.AddSection
{
   public record AddMenuSectionCommand(Guid RestuarantId, string Name):IRequest<MenuSectionDto>;
    
}
