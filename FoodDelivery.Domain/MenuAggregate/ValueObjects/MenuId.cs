using FoodDelivery.Domain.Common.Models;

namespace FoodDelivery.Domain.Menu.ValueObjects
{
    public sealed class MenuId : ValueObject
    {
        public Guid Value { get; }

        private MenuId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("MenuId cannot be empty.", nameof(value));

            Value = value;
        }

        public static MenuId CreateUnique()
            => new(Guid.NewGuid());

        public static MenuId From(Guid value)
            => new(value);

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}
