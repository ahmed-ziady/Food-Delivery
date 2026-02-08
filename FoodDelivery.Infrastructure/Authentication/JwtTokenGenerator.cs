using FoodDelivery.Application.Common.Interfaces.Authentication;
using FoodDelivery.Application.Common.Interfaces.Authentication.Services;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Infrastructure.Authentication.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FoodDelivery.Infrastructure.Authentication
{
    internal class JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> _jwtSettings) : IJwtTokenGenerator
    {
       

        public string GenerateAccessToken(User user)
        {
            var cliams = new[]
            {
                 new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                 new Claim(ClaimTypes.Email, user.Email),
                 new Claim(ClaimTypes.GivenName, user.FirstName),
                 new Claim(ClaimTypes.Surname, user.LastName)
            };
            var siningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Value.SecretKey)),
                SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(
                claims: cliams,
                issuer:_jwtSettings.Value.Issuer,
                expires: dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.Value.ExpiryMinutes),
                audience: _jwtSettings.Value.Audience,
                signingCredentials: siningCredentials);
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public RefreshToken GenerateRefreshToken(Guid userId)
        {
            return new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                ExpiresAt = dateTimeProvider.UtcNow.AddMinutes(30),
                IsRevoked = false
            };
        }
    }
}
