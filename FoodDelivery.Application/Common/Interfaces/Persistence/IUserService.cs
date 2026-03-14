using FoodDelivery.Application.Account.Common;
using FoodDelivery.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Application.Common.Interfaces.Persistence
{
    public interface IUserService
    {
        Task<IdentityResult> CreateAsync (User user,string password);
        Task<IdentityResult> DeleteAsync (Guid userId);
        Task<bool> CheckPasswordAsync (User user,string password);
        Task UpdateAsync(User user);
        Task<string> GenerateEmailTokenAsync(User user);
        Task<User?>  GetByIdAsync(Guid userId );
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task<User?> GetByEmailAsync(string Email);
    }
}
