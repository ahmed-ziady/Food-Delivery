using FoodDelivery.Application.Authentication.Interfaces;
using FoodDelivery.Application.Common.Interfaces.Services;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Infrastructure.Authentication.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FoodDelivery.Infrastructure.Authentication.Services;

internal sealed class JwtTokenGenerator(
    UserManager<User> userManager,
    RoleManager<IdentityRole<Guid>> roleManager,
    IDateTimeProvider dateTimeProvider,
    IOptions<JwtSettings> jwtSettings)
    : IJwtTokenGenerator
{
    private readonly JwtSettings _settings = jwtSettings.Value;

    public async Task<string> GenerateAccessToken(User user)
    {
        var roles = await userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty)
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));

            var roleEntity = await roleManager.FindByNameAsync(role);

            if (roleEntity is null)
                continue;

            var roleClaims = await roleManager.GetClaimsAsync(roleEntity);

            foreach (var permissionClaim in roleClaims)
            {
                if (permissionClaim.Type == "permission")
                {
                    claims.Add(permissionClaim);
                }
            }
        }

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_settings.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            expires: dateTimeProvider.UtcNow.AddMinutes(_settings.ExpiryMinutes),
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshTokenValue()
    {
        var bytes = new byte[64];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(bytes);

        return Convert.ToBase64String(bytes);
    }


}