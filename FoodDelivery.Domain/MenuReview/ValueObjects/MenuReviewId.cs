using FoodDelivery.Domain.Common.Models;

namespace FoodDelivery.Domain.MenuReview.ValueObjects
{
    public sealed class MenuReviewId : ValueObject
    {
            public MenuReviewId() { }
        public Guid Value { get; }

        private MenuReviewId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("MenuReviewId cannot be empty.", nameof(value));

            Value = value;
        }

        public static MenuReviewId CreateUnique()
            => new(Guid.NewGuid());

        public static MenuReviewId From(Guid value)
            => new(value);

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}
