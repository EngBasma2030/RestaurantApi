using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using ServiceAbstraction;
using Shared.DataTransferObject.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetByUserIdAsync(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<OrderDto> CreateAsync(CreateOrderDto dto)
        {
            var order = _mapper.Map<Order>(dto);
            await _orderRepository.AddAsync(order);
            return _mapper.Map<OrderDto>(order);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null) return false;
            await _orderRepository.DeleteAsync(order);
            return true;
        }


        //public async Task<PaginationResponse<OrderDto>> GetFilteredOrdersAsync(PaginationParams parameters)
        //{
        //    var pagedOrders = await (_orderRepository as OrderRepository)!.GetFilteredOrdersAsync(parameters);
        //    var mapped = _mapper.Map<IEnumerable<OrderDto>>(pagedOrders.Data);

        //    return new PaginationResponse<OrderDto>(
        //        mapped,
        //        pagedOrders.TotalCount,
        //        pagedOrders.PageNumber,
        //        pagedOrders.PageSize
        //    );
        //}
    }
}
