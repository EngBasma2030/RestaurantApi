using Shared.OrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IOrderItemService
    {
        Task<IEnumerable<OrderItemDto>> GetAllAsync();
        Task<OrderItemDto> GetByIdAsync(int id);
        Task<OrderItemDto> CreateAsync(CreateOrderItemDto dto);
        Task<bool> UpdateAsync(int id, UpdateOrderItemDto dto);
        Task<bool> DeleteAsync(int id);

        // دوال اضافية 
        Task<IEnumerable<OrderItemDto>> GetByOrderIdAsync(int orderId);
        Task<IEnumerable<OrderItemDto>> SearchByMenuItemNameAsync(string name);
        Task<IEnumerable<OrderItemDto>> FilterByPriceRangeAsync(decimal min, decimal max);
        Task<decimal> CalculateOrderTotalAsync(int orderId);
    }
}
