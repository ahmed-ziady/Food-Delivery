using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Infrastructure.Identity
{
    public static class PermissionPolicyRegistration
    {
        public static void RegisterPolicies(AuthorizationOptions options)
        {
            var permissions = PermissionProvider.GetAllPermissions();

            foreach (var permission in permissions)
            {
                options.AddPolicy(permission, policy =>
                    policy.RequireClaim("permission", permission));
            }
        }
    }
}
