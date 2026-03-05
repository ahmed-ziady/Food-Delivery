namespace FoodDelivery.Domain.Entities;

public class MenuItemIngredient
{
    public Guid MenuItemId { get; private set; }
    public Guid IngredientId { get; private set; }

    public MenuItem MenuItem { get; private set; } = null!;
    public Ingredient Ingredient { get; private set; } = null!;

    private MenuItemIngredient() { }

    public MenuItemIngredient(Guid menuItemId, Guid ingredientId)
    {
        MenuItemId = menuItemId;
        IngredientId = ingredientId;
    }
}