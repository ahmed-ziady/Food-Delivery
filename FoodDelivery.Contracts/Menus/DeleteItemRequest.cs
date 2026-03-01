using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Contracts.Menus
{
  public sealed record  DeleteItemRequest( Guid SectionId, Guid ItemId);
}
