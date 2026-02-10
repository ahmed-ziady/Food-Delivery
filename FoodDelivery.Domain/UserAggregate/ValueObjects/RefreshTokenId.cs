using FoodDelivery.Domain.Common.Models;

namespace FoodDelivery.Domain.UserAggregate.ValueObjects;

public sealed class RefreshTokenId : ValueObject
{
    public Guid Value { get; }

    private RefreshTokenId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("RefreshTokenId cannot be empty.");

        Value = value;
    }

    public static RefreshTokenId CreateUnique()
        => new(Guid.NewGuid());

    public static RefreshTokenId From(Guid value)
        => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
