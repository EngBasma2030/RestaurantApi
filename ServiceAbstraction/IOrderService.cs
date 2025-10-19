using Domain.Helpers;
using Domain.Responses;
using Shared.DataTransferObject.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto> GetByIdAsync(int id);
        Task<IEnumerable<OrderDto>> GetByUserIdAsync(int userId);
        Task<OrderDto> CreateAsync(CreateOrderDto dto);
        Task<bool> DeleteAsync(int id);

        //Task<PaginationResponse<OrderDto>> GetFilteredOrdersAsync(PaginationParams parameters);


    }
}
