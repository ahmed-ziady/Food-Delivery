using FoodDelivery.Domain.Common.Permissions;
using System.Reflection;

namespace FoodDelivery.Infrastructure.Identity
{
    public static class PermissionProvider
    {
        public static IEnumerable<string> GetAllPermissions()
        {
            var permissionType = typeof(Permissions);

            var nestedTypes = permissionType.GetNestedTypes();

            foreach (var type in nestedTypes)
            {
                var fields = type.GetFields(
                    BindingFlags.Public |
                    BindingFlags.Static |
                    BindingFlags.FlattenHierarchy);

                foreach (var field in fields)
                {
                    if (field.GetValue(null) is string permission)
                        yield return permission;
                }
            }
        }
    }
}
