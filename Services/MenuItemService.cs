using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using ServiceAbstraction;
using Shared.DataTransferObject.MenuItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;


        public MenuItemService(IMenuItemRepository menuItemRepository, IMapper mapper)
        {
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;

        }

        public async Task<IEnumerable<MenuItem>> GetAllAsync() => await _menuItemRepository.GetAllAsync();

        public async Task<MenuItem?> GetByIdAsync(int id) => await _menuItemRepository.GetByIdAsync(id);

        public async Task<MenuItemDto> CreateAsync(CreateMenuItemDto dto)
        {
            // نحول الـ DTO إلى كائن من نوع MenuItem باستخدام AutoMapper
            var menuItem = _mapper.Map<MenuItem>(dto);

            // نضيف العنصر في قاعدة البيانات
            await _menuItemRepository.AddAsync(menuItem);

            // نرجع نسخة DTO من العنصر بعد الإضافة
            return _mapper.Map<MenuItemDto>(menuItem);
        }
        public async Task UpdateAsync(MenuItem menuItem) => await _menuItemRepository.UpdateAsync(menuItem);
        public async Task<bool> DeleteAsync(int id)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(id);
            if (menuItem == null)
                return false;

            await _menuItemRepository.DeleteAsync(menuItem);
            return true;
        }

        // البحث والفلترة
        public async Task<IEnumerable<MenuItem>> SearchByNameAsync(string name) => await _menuItemRepository.SearchByNameAsync(name);
        public async Task<IEnumerable<MenuItem>> FilterByCategoryAsync(string category) => await _menuItemRepository.FilterByCategoryAsync(category);
        public async Task<IEnumerable<MenuItem>> FilterByAvailabilityAsync(bool isAvailable) => await _menuItemRepository.FilterByAvailabilityAsync(isAvailable);
        public async Task<IEnumerable<MenuItem>> FilterByPriceRangeAsync(decimal minPrice, decimal maxPrice) => await _menuItemRepository.FilterByPriceRangeAsync(minPrice, maxPrice);

        //public Task<MenuItem> AddAsync(MenuItem menuItem)
        //{
        //    throw new NotImplementedException();
        //}

        //Task IMenuItemService.DeleteAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
