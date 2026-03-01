using FoodDelivery.Domain.Commons;
using FoodDelivery.Domain.Commons.Exceptions;
using System.Collections.Immutable;

namespace FoodDelivery.Domain.Entities
{
    public class MenuSection
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; } = null!;

        private readonly List<MenuItem> _items = [];
        public IReadOnlyCollection<MenuItem> Items => _items.AsReadOnly();

        private MenuSection() { } // EF

        public MenuSection(string name)
        {
            SetName(name);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Menu section name cannot be empty.");
            Name = name;
        }

        public void AddItem(MenuItem item)
        {
            if (_items.Any(i => i.Name.Equals(item.Name, StringComparison.OrdinalIgnoreCase)))
                throw new DomainException($"Item '{item.Name}' already exists in this section.");
            _items.Add(item);
        }

        public void RemoveItem(Guid id)
        {
            var item = _items.FirstOrDefault(i => i.Id == id)
                       ?? throw new MenuItemNotFoundException(id);
            _items.Remove(item);
        }

        public void UpdateItem(Guid id, string? name, string? description, decimal? price)
        {
            var item = _items.FirstOrDefault(i => i.Id == id)
                       ?? throw new MenuItemNotFoundException(id);

            if (_items.Any(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && i.Id != id))
                throw new DomainException($"Another menu item with name '{name}' already exists in this section.");

            item.UpdateDetails(name, price, description);
        }

        public MenuItem? GetItem(Guid itemId) => _items.FirstOrDefault(i => i.Id == itemId);
    }
}

