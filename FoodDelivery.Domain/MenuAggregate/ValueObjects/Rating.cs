using FoodDelivery.Domain.Common.Models;

namespace FoodDelivery.Domain.MenuAggregate.ValueObjects
{
    public sealed class Rating : ValueObject
    {
        public int Value { get; }

        private Rating() { } // EF Core only

        private Rating(int value)
        {
            if (value is < 1 or > 5)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "Rating must be between 1 and 5.");

            Value = value;
        }

        public static Rating Create(int value)
            => new(value);

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}