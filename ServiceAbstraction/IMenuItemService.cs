using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface IMenuItemService
    {
        Task<IEnumerable<MenuItem>> GetAllAsync();
        Task<MenuItem?> GetByIdAsync(int id);
        //Task<MenuItem> AddAsync(MenuItem menuItem);
        Task UpdateAsync(MenuItem menuItem);
        //Task DeleteAsync(int id);

        // البحث والفلترة
        Task<IEnumerable<MenuItem>> SearchByNameAsync(string name);
        Task<IEnumerable<MenuItem>> FilterByCategoryAsync(string category);
        Task<IEnumerable<MenuItem>> FilterByAvailabilityAsync(bool isAvailable);
        Task<IEnumerable<MenuItem>> FilterByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    }
}

