using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IMenuItemRepository : IGenericRepository<MenuItem>
    {
        Task<IEnumerable<MenuItem>> SearchByNameAsync(string name);
        Task<IEnumerable<MenuItem>> FilterByCategoryAsync(string category);
        Task<IEnumerable<MenuItem>> FilterByAvailabilityAsync(bool isAvailable);
        Task<IEnumerable<MenuItem>> FilterByPriceRangeAsync(decimal minPrice, decimal maxPrice);
    }
}
