namespace FoodDelivery.Domain.Commons.Exceptions
{
    public class IngredientNotFoundException : BusinessRuleException
    {
        public IngredientNotFoundException(string ingredientName)
            : base($"Ingredient '{ingredientName}' not found in the menu item.",
                   "INGREDIENT_NOT_FOUND")
        {
        }
    }
}
