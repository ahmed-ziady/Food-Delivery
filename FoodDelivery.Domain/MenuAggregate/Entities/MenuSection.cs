using FoodDelivery.Domain.Common.Models;
using FoodDelivery.Domain.Common.ValueObjects;
using FoodDelivery.Domain.Menu.Entities;
using FoodDelivery.Domain.Menu.ValueObjects;

namespace FoodDelivery.Domain.MenuAggregate.Entities
{
    public sealed class MenuSection : Entity<MenuSectionId>
    {
        private readonly List<MenuItem> _menuItems = [];

        private MenuSection(
            MenuSectionId id,
            string name,
            string description) : base(id)
        {
            Name = name;
            Description = description;
            AverageRating = AverageRating.Empty();
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public AverageRating AverageRating { get; private set; }

        public IReadOnlyCollection<MenuItem> MenuItems => _menuItems.AsReadOnly();

        // ---------- Factory ----------
        public static MenuSection Create(string name, string description)
        {
            

            return new MenuSection(
                MenuSectionId.CreateUnique(),
                name.Trim(),
                description?.Trim() ?? string.Empty);
        }

        // ---------- Behavior ----------
        public void Rename(string name)
        {
            

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

        public void AddMenuItem(MenuItem item)
        {
            ArgumentNullException.ThrowIfNull(item);

            if (_menuItems.Any(i => i.Name == item.Name))
                throw new InvalidOperationException("Menu item with same name already exists.");

            _menuItems.Add(item);
        }

        public void RemoveMenuItem(MenuItem item)
        {
            ArgumentNullException.ThrowIfNull(item);

            if (!_menuItems.Remove(item))
                throw new InvalidOperationException("Menu item not found.");
        }

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
    }
}
