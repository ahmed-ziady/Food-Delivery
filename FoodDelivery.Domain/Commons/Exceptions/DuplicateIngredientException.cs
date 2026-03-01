namespace FoodDelivery.Domain.Commons.Exceptions
{
    public sealed class DuplicateIngredientException(string name) : BusinessRuleException($"Ingredient '{name}' already exists.",
               "DUPLICATE_INGREDIENT")
    {
    }
}
