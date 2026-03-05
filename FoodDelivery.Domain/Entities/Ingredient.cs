using FoodDelivery.Domain.Commons;
using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Domain.Entities;

public class Ingredient
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string ImageUrl { get; private set; } = null!;
    public IngredientType Type { get; private set; }

    private readonly HashSet<MenuItemIngredient> _menuItemIngredients = new();
    public IReadOnlyCollection<MenuItemIngredient> MenuItemIngredients => _menuItemIngredients.AsReadOnly();

    private Ingredient() { }

    public Ingredient(string name, string imageUrl, IngredientType type)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Ingredient name required.");

        if (string.IsNullOrWhiteSpace(imageUrl))
            throw new DomainException("Ingredient image required.");

        Id = Guid.NewGuid();
        Name = name.Trim();
        ImageUrl = imageUrl.Trim();
        Type = type;
    }
}