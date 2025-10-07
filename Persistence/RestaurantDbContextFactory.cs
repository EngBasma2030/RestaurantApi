using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class RestaurantDbContextFactory : IDesignTimeDbContextFactory<RestaurantDbContext>
    {
        public RestaurantDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RestaurantDbContext>();

            optionsBuilder.UseSqlServer("Server=.;Database=RestaurantDb;Trusted_Connection=True;TrustServerCertificate=True;");

            return new RestaurantDbContext(optionsBuilder.Options);
        }
    }
}
