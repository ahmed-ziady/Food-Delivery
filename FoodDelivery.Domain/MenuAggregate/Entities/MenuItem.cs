using FoodDelivery.Domain.Common.Models;
using FoodDelivery.Domain.Common.ValueObjects;
using FoodDelivery.Domain.MenuAggregate.ValueObjects;

namespace FoodDelivery.Domain.MenuAggregate.Entities
{
    public sealed class MenuItem : Entity<MenuItemId>
    {
        private MenuItem() { } // EF Core

        private MenuItem(
            MenuItemId id,
            string name,
            string description,
            Price price) : base(id)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public string Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public Price Price { get; private set; } = null!;
        public AverageRating AverageRating { get; private set; } = null!;

        public static MenuItem Create(
            string name,
            string description,
            Price price
            )
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Menu item name is required.", nameof(name));

            ArgumentNullException.ThrowIfNull(price);

            return new MenuItem(
                MenuItemId.CreateUnique(),
                name.Trim(),
                description?.Trim() ?? string.Empty,
                price);
        }

        // -------- Behavior --------
        public void AddRating(Rating rating)
        {
            ArgumentNullException.ThrowIfNull(rating);
            AverageRating = AverageRating.AddRating(rating);
        }

        public void RemoveRating(Rating rating)
        {
            ArgumentNullException.ThrowIfNull(rating);
            AverageRating = AverageRating.RemoveRating(rating);
        }

        public void ChangePrice(Price newPrice)
        {
            ArgumentNullException.ThrowIfNull(newPrice);

            if (Price == newPrice)
                return;

            Price = newPrice;
        }

        public void Rename(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Menu item name is required.", nameof(name));

            name = name.Trim();

            if (Name == name)
                return;

            Name = name;
        }

        public void UpdateDescription(string description)
        {
            description = description?.Trim() ?? string.Empty;

            if (Description == description)
                return;

            Description = description;
        }
    }
}