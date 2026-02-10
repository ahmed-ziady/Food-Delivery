using System;
using System.Collections.Generic;
using FoodDelivery.Domain.Common.Models;

namespace FoodDelivery.Domain.UserAggregate.ValueObjects;

public sealed class UserId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private UserId() { } // For EF Core

    private UserId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("UserId cannot be empty.", nameof(value));

        Value = value;
    }

    public static UserId CreateUnique()
        => new(Guid.NewGuid());

    public static UserId From(Guid value)
        => new(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
