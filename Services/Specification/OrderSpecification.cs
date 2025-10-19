using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specification
{
    public class OrderSpecification
    {
        public static IQueryable<Order> ApplyFilters(IQueryable<Order> query, string? status, string? searchTerm)
        {
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(o => o.Status.ToLower() == status.ToLower());
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(o =>
                    o.UserId.ToString().Contains(searchTerm) ||
                    o.Status.Contains(searchTerm));
            }

            return query;
        }
    }
}
