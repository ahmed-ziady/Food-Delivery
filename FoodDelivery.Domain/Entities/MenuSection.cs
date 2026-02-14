using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Domain.Entities
{
    public class MenuSection
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }
        public string? Description { get; private set; }

        public Guid MenuId { get; private set; }

        public List<MenuItem> MenuItems { get; private set; } = new();

        private MenuSection() { }

        public MenuSection(string name, string? description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
        }

        public void AddItem(string name, string? description, decimal price)
        {
            MenuItems.Add(new MenuItem(name, description, price));
        }
    }

}
