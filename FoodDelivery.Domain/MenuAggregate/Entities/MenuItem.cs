using FoodDelivery.Domain.Common.Models;
using FoodDelivery.Domain.Menu.ValueObjects;

namespace FoodDelivery.Domain.Menu.Entities
{
    public sealed class MenuItem : Entity<MenuItemId>
    {
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

        public string Name { get; private set; }
        public string Description { get; private set; }
        public Price Price { get; private set; }

        public static MenuItem Create(
            string name,
            string description,
            Price price)
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
