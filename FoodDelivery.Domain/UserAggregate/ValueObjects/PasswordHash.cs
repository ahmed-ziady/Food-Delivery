using System;
using System.Collections.Generic;
using FoodDelivery.Domain.Common.Models;

namespace FoodDelivery.Domain.UserAggregate.ValueObjects;

public sealed class PasswordHash : ValueObject
{
    public string Value { get; }

#pragma warning disable CS8618
    private PasswordHash() { } // For EF Core

    private PasswordHash(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Password hash is required.", nameof(value));

        Value = value;
    }

    public static PasswordHash Create(string hash)
        => new(hash);

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
