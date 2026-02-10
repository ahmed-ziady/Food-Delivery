using System;
using System.Collections.Generic;
using System.Net.Mail;
using FoodDelivery.Domain.Common.Models;

namespace FoodDelivery.Domain.UserAggregate.ValueObjects;

public sealed class Email : ValueObject
{
    public string Value { get; }

#pragma warning disable CS8618
    private Email() { } // For EF Core

    private Email(string value)
    {
        Value = NormalizeAndValidate(value);
    }

    public static Email Create(string value)
        => new(value);

    public override string ToString() => Value;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    private static string NormalizeAndValidate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email is required.", nameof(value));

        var normalized = value.Trim().ToLowerInvariant();

        try
        {
            var addr = new MailAddress(normalized);
            if (!string.Equals(addr.Address, normalized, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Invalid email format.", nameof(value));
        }
        catch (FormatException)
        {
            throw new ArgumentException("Invalid email format.", nameof(value));
        }

        return normalized;
    }
}
