using FoodDelivery.Domain.Common.Models;

namespace FoodDelivery.Domain.MenuAggregate.ValueObjects
{
    public sealed class AverageRating : ValueObject
    {
        public double Value { get; }
        public int NumberOfRatings { get; }

        private AverageRating() { } // EF Core only

        private AverageRating(double value, int numberOfRatings)
        {
            Value = value;
            NumberOfRatings = numberOfRatings;
        }

        public static AverageRating Empty()
            => new(0, 0);

        public static AverageRating Create(double value, int numberOfRatings)
        {
            if (value < 0 || value > 5)
                throw new ArgumentOutOfRangeException(nameof(value),
                    "Average rating must be between 0 and 5.");

            if (numberOfRatings < 0)
                throw new ArgumentOutOfRangeException(nameof(numberOfRatings),
                    "Number of ratings cannot be negative.");

            return new AverageRating(value, numberOfRatings);
        }

        public AverageRating AddRating(Rating rating)
        {
            ArgumentNullException.ThrowIfNull(rating);

            var total = Value * NumberOfRatings + rating.Value;
            var newCount = NumberOfRatings + 1;
            var newAverage = total / newCount;

            return new AverageRating(newAverage, newCount);
        }

        public AverageRating RemoveRating(Rating rating)
        {
            ArgumentNullException.ThrowIfNull(rating);

            if (NumberOfRatings == 0)
                throw new InvalidOperationException("No ratings to remove.");

            var total = Value * NumberOfRatings - rating.Value;
            var newCount = NumberOfRatings - 1;
            var newAverage = newCount == 0 ? 0 : total / newCount;

            return new AverageRating(newAverage, newCount);
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
            yield return NumberOfRatings;
        }

        public override string ToString()
            => NumberOfRatings == 0
                ? "No ratings yet"
                : $"{Value:F1} ({NumberOfRatings} ratings)";
    }
}