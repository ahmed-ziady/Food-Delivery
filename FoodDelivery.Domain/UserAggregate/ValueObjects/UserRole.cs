using FoodDelivery.Domain.Common.Models;

namespace FoodDelivery.Domain.UserAggregate.ValueObjects;

public sealed class UserRole : ValueObject
{
    public string Name { get; }

    private UserRole(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role name is required.");

        Name = name;
    }

    public static readonly UserRole Admin = new("Admin");
    public static readonly UserRole Chef = new("Chef");
    public static readonly UserRole Delivery = new("Delivery");
    public static readonly UserRole Customer = new("Customer");

    public static UserRole From(string name)
        => new(name);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }

    public override string ToString() => Name;
}
