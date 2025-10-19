using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using ServiceAbstraction;
using Shared.OrderItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IMapper _mapper;

        public OrderItemService(IOrderItemRepository orderItemRepository, IMapper mapper)
        {
            _orderItemRepository = orderItemRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderItemDto>> GetAllAsync()
        {
            var items = await _orderItemRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderItemDto>>(items);
        }

        public async Task<OrderItemDto> GetByIdAsync(int id)
        {
            var item = await _orderItemRepository.GetByIdAsync(id);
            return _mapper.Map<OrderItemDto>(item);
        }

        public async Task<OrderItemDto> CreateAsync(CreateOrderItemDto dto)
        {
            var item = _mapper.Map<OrderItem>(dto);
            await _orderItemRepository.AddAsync(item);
            return _mapper.Map<OrderItemDto>(item);
        }

        public async Task<bool> UpdateAsync(int id, UpdateOrderItemDto dto)
        {
            var existing = await _orderItemRepository.GetByIdAsync(id);
            if (existing == null) return false;

            _mapper.Map(dto, existing);
            await _orderItemRepository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = _orderItemRepository.GetByIdAsync(id);
            if (existing == null) return false;

            _orderItemRepository.DeleteAsync(await existing);
            return true;
        }

        public async Task<IEnumerable<OrderItemDto>> GetByOrderIdAsync(int orderId)
        {
            var items = await _orderItemRepository.GetByOrderIdAsync(orderId);
            return _mapper.Map<IEnumerable<OrderItemDto>>(items);
        }

        public async Task<IEnumerable<OrderItemDto>> SearchByMenuItemNameAsync(string name)
        {
            var items = await _orderItemRepository.SearchByMenuItemNameAsync(name);
            return _mapper.Map<IEnumerable<OrderItemDto>>(items);
        }

        public async Task<IEnumerable<OrderItemDto>> FilterByPriceRangeAsync(decimal min, decimal max)
        {
            var items = await _orderItemRepository.FilterByPriceRangeAsync(min, max);
            return _mapper.Map<IEnumerable<OrderItemDto>>(items);
        }
        public async Task<decimal> CalculateOrderTotalAsync(int orderId)
        {
            return await _orderItemRepository.CalculateOrderTotalAsync(orderId);
        }

    }
}
