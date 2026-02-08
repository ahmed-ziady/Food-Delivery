using FoodDelivery.Domain.Common.Models;

namespace FoodDelivery.Domain.Dinner.ValueObjects
{
    public sealed class DinnerId : ValueObject
    {
        public Guid Value { get; }

        private DinnerId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("DinnerId cannot be empty.", nameof(value));

            Value = value;
        }

        public static DinnerId CreateUnique()
            => new(Guid.NewGuid());

        public static DinnerId From(Guid value)
            => new(value);

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}
