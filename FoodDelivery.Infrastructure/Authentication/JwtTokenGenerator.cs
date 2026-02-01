using FoodDelivery.Application.Common.Interfaces.Authentication;
using FoodDelivery.Application.Common.Interfaces.Authentication.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodDelivery.Infrastructure.Authentication
{
    internal class JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> _jwtSettings) : IJwtTokenGenerator
    {
        public string GenerateToken(Guid Id, string firstName, string lastName, string email)
        {
            var cliams = new[]
            {
                 new Claim(ClaimTypes.NameIdentifier,Id.ToString()),
                 new Claim(ClaimTypes.Email, email),
                 new Claim(ClaimTypes.GivenName, firstName),
                 new Claim(ClaimTypes.Surname, lastName)
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
    }
}
