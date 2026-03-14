using FoodDelivery.Domain.Commons.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Domain.Common.Exceptions
{
    public class TooManyImagesException(int maxAllowed) : BusinessRuleException($"A menu item cannot have more than {maxAllowed} images.",
               "TOO_MANY_IMAGES")
    {
    }
}
