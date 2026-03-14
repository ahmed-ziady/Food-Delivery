using FoodDelivery.Domain.Common.Permissions;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FoodDelivery.Infrastructure.Identity
{
    public static class RolePermissionSeeder
    {
        public static async Task SeedAsync(
            RoleManager<IdentityRole<Guid>> roleManager)
        {
            await CreateRole(roleManager, Roles.Admin,
            [
                Permissions.User.Manage,
            Permissions.Restaurant.Manage,
            Permissions.Menu.Create,
            Permissions.Menu.Update,
            Permissions.Menu.Delete,
            Permissions.Menu.View,
            Permissions.Ingredient.Delete,
            Permissions.Ingredient.View,
            Permissions.Ingredient.Create
            ]);

            await CreateRole(roleManager, Roles.Chef,
            [
                Permissions.MenuSection.Create,
            Permissions.MenuSection.Update,
            Permissions.MenuSection.Delete,
            Permissions.MenuSection.View,
            Permissions.MenuItem.Create,
            Permissions.MenuItem.Update,
            Permissions.MenuItem.Delete,
            Permissions.MenuItem.View,
            Permissions.Ingredient.View,
            Permissions.Ingredient.Use
            ]);

            await CreateRole(roleManager, Roles.User,
            [
            Permissions.Menu.View,
            Permissions.Order.Create,
            Permissions.Order.Cancel
            ]);
        }

        private static async Task CreateRole(
            RoleManager<IdentityRole<Guid>> roleManager,
            string roleName,
            string[] permissions)
        {
            var role = await roleManager.FindByNameAsync(roleName);

            if (role == null)
            {
                role = new IdentityRole<Guid>(roleName);
                await roleManager.CreateAsync(role);
            }

            var existingClaims = await roleManager.GetClaimsAsync(role);

            foreach (var permission in permissions)
            {
                if (!existingClaims.Any(c =>
                    c.Type == "permission" &&
                    c.Value == permission))
                {
                    await roleManager.AddClaimAsync(
                        role,
                        new Claim("permission", permission));
                }
            }
        }
    }
}
