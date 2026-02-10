using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace FoodDelivery.Domain.Common.Models
{
    public abstract class AggregateRootId<TId> : ValueObject
    {
        public abstract TId Value { get; protected set; }
        
    }
}
