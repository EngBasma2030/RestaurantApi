using Domain.Contracts;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        private readonly RestaurantDbContext _context;

        public PaymentRepository(RestaurantDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payment>> GetByMethodAsync(string method)
        {
            return await _context.Payments
                .Where(p => p.Method.ToLower() == method.ToLower())
                .ToListAsync();

        }

        public async Task<IEnumerable<Payment>> GetByOrderIdAsync(int orderId)
        {
            return await _context.Payments
                            .Where(p => p.OrderId == orderId)
                            .ToListAsync();
        }
    }
}
