using FoodDelivery.Domain.Common.Models;

namespace FoodDelivery.Domain.UserAggregate.ValueObjects;

public sealed class PhoneNumber : ValueObject
{
    public string Value { get; }

    private PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Phone number is required.");

        Value = value.Trim();
    }

    public static PhoneNumber Create(string value)
        => new(value);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
