using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Domain.Commons.Exceptions
{
    public class ImageNotFoundException(string url) : BusinessRuleException($"Image with URL '{url}' was not found.",
               "IMAGE_NOT_FOUND")
    {
    }
}
