using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Contracts.Sections
{
    
    public sealed record UpdateItemDetailsRequest(
        string? Name,
        string? Description,
        decimal? Price
);
}
