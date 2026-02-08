using FoodDelivery.Application.Common.Interfaces.Persistence;
using FoodDelivery.Domain.MenuAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Infrastructure.Persistence
{
    public class MenuRepository : IMenuRepository
    {
        private static readonly List<Menu> _menus = [];
        public void Add(Menu menu)
        {
           _menus.Add(menu);
        }
    }
}
