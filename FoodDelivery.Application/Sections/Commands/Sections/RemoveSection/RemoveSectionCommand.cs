using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Menus.Commands.Sections.RemoveSection
{
    public sealed record   RemoveSectionCommand(Guid restaurantId , Guid SectionId):IRequest<Unit>;

    
}
