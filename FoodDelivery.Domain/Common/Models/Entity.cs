namespace FoodDelivery.Domain.Common.Models
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
        where TId : notnull
    {
        public TId Id { get; protected set; }

        protected Entity(TId id)
        {
            Id = id;
        }

        protected Entity()
        {
            // Required by ORM
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Entity<TId> other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (IsTransient() || other.IsTransient())
                return false;

            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        public bool Equals(Entity<TId>? other)
            => Equals((object?)other);

        public override int GetHashCode()
        {
            if (IsTransient())
                return base.GetHashCode();

            return HashCode.Combine(GetType(), Id);
        }

        public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
            => !(left == right);

        private bool IsTransient()
        {
            return EqualityComparer<TId>.Default.Equals(Id, default!);
        }
    }
}
