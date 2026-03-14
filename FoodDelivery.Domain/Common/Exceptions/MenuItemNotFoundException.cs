namespace FoodDelivery.Domain.Commons.Exceptions
{
    public class MenuItemNotFoundException(Guid menuItemId) : BusinessRuleException($"Menu item with ID '{menuItemId}' not found.",
               "MENU_ITEM_NOT_FOUND") 
    {
    }
}
