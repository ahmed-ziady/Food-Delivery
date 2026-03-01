using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Contracts.Menus
{
    
    public sealed record UpdateItemDetailsRequest(
        string? Name,
        string? Description,
        decimal? Price
);
}
