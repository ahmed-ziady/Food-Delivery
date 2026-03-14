namespace FoodDelivery.Domain.Common.Permissions;

public static class Permissions
{
    public static class Menu
    {
        public const string Create = "menu.create";
        public const string Update = "menu.update";
        public const string Delete = "menu.delete";
        public const string View = "menu.view";
    }

    public static class MenuSection
    {
        public const string Create = "menuSection.create";
        public const string Update = "menuSection.update";
        public const string Delete = "menuSection.delete";
        public const string View = "menuSection.view";
    }

    public static class MenuItem
    {
        public const string Create = "menuItem.create";
        public const string Update = "menuItem.update";
        public const string Delete = "menuItem.delete";
        public const string View = "menuItem.view";
    }

    public static class Ingredient
    {
        public const string Create = "ingredient.create";
        public const string Delete = "ingredient.delete";
        public const string Use = "ingredient.use";
        public const string View = "ingredient.view";
    }

    public static class Order
    {
        public const string Create = "order.create";
        public const string Cancel = "order.cancel";
        public const string Manage = "order.manage";
    }

    public static class Restaurant
    {
        public const string Manage = "restaurant.manage";
    }

    public static class User
    {
        public const string Manage = "user.manage";
    }
}