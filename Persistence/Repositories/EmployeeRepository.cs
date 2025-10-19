using Domain.Contracts;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RestaurantDbContext context) : base(context)
        {
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
