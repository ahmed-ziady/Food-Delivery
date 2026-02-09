using FoodDelivery.Domain.Common.Models;

namespace FoodDelivery.Domain.MenuAggregate.ValueObjects
{
    public sealed class MenuSectionId : ValueObject
    {
        public MenuSectionId() { }
        public Guid Value { get; }

        private MenuSectionId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("MenuSectionId cannot be empty.", nameof(value));

            Value = value;
        }

        public static MenuSectionId CreateUnique()
            => new(Guid.NewGuid());

        public static MenuSectionId From(Guid value)
            => new(value);

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}
