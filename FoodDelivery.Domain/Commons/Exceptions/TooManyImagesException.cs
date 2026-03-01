using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Domain.Commons.Exceptions
{
    public class TooManyImagesException: BusinessRuleException
    {
        public TooManyImagesException(int maxAllowed)
            : base($"A menu item cannot have more than {maxAllowed} images.",
                   "TOO_MANY_IMAGES")
        {
        }
    }
}
