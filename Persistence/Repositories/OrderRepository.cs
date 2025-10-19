using Domain.Contracts;
using Domain.Helpers;
using Domain.Models;
using Domain.Responses;
using Microsoft.EntityFrameworkCore;
using Services.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Persistence.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly RestaurantDbContext _context;
        public OrderRepository(RestaurantDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

        public async Task<PaginationResponse<Order>> GetFilteredOrdersAsync(PaginationParams parameters)
        {
            var query = _context.Orders.AsQueryable();

            // نطبق الـ Specification
            query = OrderSpecification.ApplyFilters(query, parameters.Status, parameters.SearchTerm);

            var totalCount = await query.CountAsync();

            var data = await query
                .OrderByDescending(o => o.OrderDate)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .Include(o => o.User)
                .ToListAsync();

            return new PaginationResponse<Order>(data, totalCount, parameters.PageNumber, parameters.PageSize);
        }
    }
}
