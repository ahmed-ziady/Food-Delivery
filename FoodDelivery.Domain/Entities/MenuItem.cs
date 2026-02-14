using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Domain.Entities
{
    public class MenuItem
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; }
        public string? Description { get; private set; }

        public decimal Price { get; private set; }

        public Guid MenuSectionId { get; private set; }

        private MenuItem() { }

        public MenuItem(string name, string? description, decimal price)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
        }
    }

}
