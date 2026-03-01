using FoodDelivery.Domain.Commons;
using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Domain.ValueObjects
{
    public class Ingredient
    {
        public string Name { get; private set; } = null!;
        public string ImageUrl { get; private set; } = null!;
        public IngredientType Type { get; private set; }

        private Ingredient() { }
        public Ingredient(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Ingredient name cannot be empty.");
            Name = name;
        }
    }
}
