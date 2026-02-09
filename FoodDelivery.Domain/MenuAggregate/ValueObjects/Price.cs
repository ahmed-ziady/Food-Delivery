using FoodDelivery.Domain.Common.Models;

namespace FoodDelivery.Domain.MenuAggregate.ValueObjects
{
    public sealed class Price : ValueObject
    {
        public Price() { }
        public decimal Amount { get; private set; }

        private Price(decimal amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount),
                    "Price amount cannot be negative.");

            Amount = amount;
        }

        public static Price Create(decimal amount)
            => new(amount);

        public Price Add(Price other)
        {
            ArgumentNullException.ThrowIfNull(other);
            return new Price(Amount + other.Amount);
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Amount;
        }

        public override string ToString()
            => Amount.ToString("0.00");
    }
}
