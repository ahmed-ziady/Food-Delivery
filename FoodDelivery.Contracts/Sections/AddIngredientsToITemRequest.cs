namespace FoodDelivery.Contracts.Sections
{
    public sealed record AddIngredientsToITemRequest(List<Guid> IngredientIds);
}