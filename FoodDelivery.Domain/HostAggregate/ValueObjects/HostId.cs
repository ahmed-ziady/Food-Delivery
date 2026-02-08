using FoodDelivery.Domain.Common.Models;

namespace FoodDelivery.Domain.Host.ValueObjects
{
    public sealed class HostId : ValueObject
    {
        public Guid Value { get; }

        private HostId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("HostId cannot be empty.", nameof(value));

            Value = value;
        }

        public static HostId CreateUnique()
            => new(Guid.NewGuid());

        public static HostId From(Guid value)
            => new(value);

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value.ToString();
    }
}
