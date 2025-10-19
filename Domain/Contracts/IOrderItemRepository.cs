using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IOrderItemRepository : IGenericRepository<OrderItem>
    {
        Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId);  // جلب العناصر الخاصة بطلب معين
        Task<IEnumerable<OrderItem>> SearchByMenuItemNameAsync(string name); // البحث باسم الوجبة
        Task<decimal> CalculateOrderTotalAsync(int orderId); // حساب الإجمالي للطلب
        Task<IEnumerable<OrderItem>> FilterByPriceRangeAsync(decimal min, decimal max); // فلترة حسب السعر
    }
}
