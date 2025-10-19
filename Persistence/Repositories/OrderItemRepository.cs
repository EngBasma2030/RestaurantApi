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
    public class OrderItemRepository : GenericRepository<OrderItem>, IOrderItemRepository
    {
        private readonly RestaurantDbContext _context;

        public OrderItemRepository(RestaurantDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId)
        {
            return await _context.OrderItems
                .Include(o => o.MenuItem)
                .Where(o => o.OrderId == orderId)
                .ToListAsync();
        }

        public async Task<IEnumerable<OrderItem>> SearchByMenuItemNameAsync(string name)
        {
            return await _context.OrderItems
                .Include(o => o.MenuItem)
                .Where(o => o.MenuItem.Name.Contains(name))
                .ToListAsync();
        }
        public async Task<decimal> CalculateOrderTotalAsync(int orderId)
        {
            return await _context.OrderItems
                .Where(o => o.OrderId == orderId)
                .SumAsync(o => o.SubTotal);
        }

        public async Task<IEnumerable<OrderItem>> FilterByPriceRangeAsync(decimal min, decimal max)
        {
            return await _context.OrderItems
                .Include(o => o.MenuItem)
                .Where(o => o.SubTotal >= min && o.SubTotal <= max)
                .ToListAsync();
        }

    }
}
