namespace FoodDelivery.Domain.Commons.Exceptions
{
    public sealed class IngredientNameRequiredException : BusinessRuleException
    {
        public IngredientNameRequiredException()
            : base("Ingredient name is required.",
                   "INGREDIENT_NAME_REQUIRED")
        {
        }
    }
}
