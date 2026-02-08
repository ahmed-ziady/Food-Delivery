using FoodDelivery.Domain.Common.Models;

namespace FoodDelivery.Domain.Menu.ValueObjects
{
    public sealed class MenuItemId : ValueObject
    {
        public Guid Value { get; }

        private MenuItemId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("MenuItemId cannot be empty.", nameof(value));

            Value = value;
        }

        public static MenuItemId CreateUnique()
            => new(Guid.NewGuid());

        public static MenuItemId From(Guid value)
            => new(value);

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}
