using FoodDelivery.Domain.Common.Models;

namespace FoodDelivery.Domain.Common.ValueObjects
{
    /// <summary>
    /// Represents a rating value between 1 and 5.
    /// </summary>
    public sealed class Rating : ValueObject
    {
            public Rating() { }
        public int Value { get; }

        private Rating(int value)
        {
            if (value is < 1 or > 5)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "Rating must be between 1 and 5.");

            Value = value;
        }

        public static Rating Create(int value)
            => new(value);

        public bool IsMax => Value == 5;
        public bool IsMin => Value == 1;

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}
