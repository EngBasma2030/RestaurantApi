using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class MenuItemRepository : GenericRepository<MenuItem>, IMenuItemRepository
    {
        private readonly RestaurantDbContext _context;

        public MenuItemRepository(RestaurantDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<MenuItem> AddAsync(MenuItem menuItem)
        {
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();
            return menuItem;
        }
        public async Task<IEnumerable<MenuItem>> SearchByNameAsync(string name)
        {
            return await _context.MenuItems
                .Where(m => m.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> FilterByCategoryAsync(string category)
        {
            return await _context.MenuItems
                .Where(m => m.Category.ToLower() == category.ToLower())
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> FilterByAvailabilityAsync(bool isAvailable)
        {
            return await _context.MenuItems
                .Where(m => m.IsAvailable == isAvailable)
                .ToListAsync();
        }

        public async Task<IEnumerable<MenuItem>> FilterByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _context.MenuItems
                .Where(m => m.Price >= minPrice && m.Price <= maxPrice)
                .ToListAsync();
        }

    }
}
