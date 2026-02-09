using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Domain.MenuAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Infrastructure.Persistence.Repositories
{
    public class MenuRepository(FoodDeliveryDbContext _dbContext) : IMenuRepository
    {
      
        public void Add(Menu menu)
        {
            _dbContext.Add(menu);
            _dbContext.SaveChanges();
        }
    }
}
