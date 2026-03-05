using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Menus.Commands.Items.DeleteItem
{
    public sealed record  DeleteItemCommand (Guid RestuarantId, Guid SectionId, Guid ItemId) :IRequest<Unit>;
   
}
