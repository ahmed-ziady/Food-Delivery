using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Infrastructure.Persistence
{
    internal class UserRepository : IUserRepository
    {
        private static List<User> _users = [];
        public void Add(User user)
        {
          _users.Add(user);
        }

        public User? GetUserByEmail(string email)
        {
           return _users.SingleOrDefault(u=>u.Email == email);
        }
    }
}
