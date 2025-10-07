using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class DbInitializer
    {
        public static void Initialize(RestaurantDbContext context)
        {

            // Use transaction so partial inserts are rolled back if شيء غلط
            using var tx = context.Database.BeginTransaction();
            try
            {
                // ---- Users ----
                if (!context.Users.Any())
                {
                    context.Users.AddRange(
                        new User { FullName = "Admin", Email = "admin@restaurant.com", PasswordHash = "adminhashed", Role = "Admin" },
                        new User { FullName = "Basma Bahaa", Email = "basma@example.com", PasswordHash = "passhash", Role = "Customer" }
                    );
                    context.SaveChanges();
                }

                // ---- Employees ----
                if (!context.Employees.Any())
                {
                    context.Employees.AddRange(
                        new Employee { Name = "محمد أحمد", Position = "Chef", Salary = 7000.00m },
                        new Employee { Name = "علي حسن", Position = "Waiter", Salary = 4000.00m },
                        new Employee { Name = "نهى مصطفى", Position = "Cashier", Salary = 4500.00m }
                    );
                    context.SaveChanges();
                }

                // ---- MenuItems ----
                if (!context.MenuItems.Any())
                {
                    context.MenuItems.AddRange(
                        new MenuItem { Name = "بيتزا مارجريتا", Description = "Classic", Price = 120.00m, Category = "Main", IsAvailable = true },
                        new MenuItem { Name = "بيتزا دجاج", Description = "With chicken", Price = 150.00m, Category = "Main", IsAvailable = true },
                        new MenuItem { Name = "كباب", Description = "Grill", Price = 200.00m, Category = "Grill", IsAvailable = true },
                        new MenuItem { Name = "كولا", Description = "Cold drink", Price = 25.00m, Category = "Drinks", IsAvailable = true }
                    );
                    context.SaveChanges();
                }

                // ---- Orders ----
                if (!context.Orders.Any())
                {
                    // get a user to attach orders to
                    var customer = context.Users.FirstOrDefault(u => u.Role == "Customer") ?? context.Users.First();
                    context.Orders.AddRange(
                        new Order { UserId = customer.Id, OrderDate = new DateTime(2024, 01, 15), TotalAmount = 270.00m, Status = "Completed" },
                        new Order { UserId = customer.Id, OrderDate = new DateTime(2024, 02, 10), TotalAmount = 175.00m, Status = "Pending" }
                    );
                    context.SaveChanges();
                }

                // ---- OrderItems ----
                if (!context.OrderItems.Any())
                {
                    var order1 = context.Orders.OrderBy(o => o.Id).First();
                    var order2 = context.Orders.OrderByDescending(o => o.Id).First();

                    // pick some menu items
                    var m1 = context.MenuItems.FirstOrDefault(mi => mi.Name.Contains("مارجريتا")) ?? context.MenuItems.First();
                    var m2 = context.MenuItems.FirstOrDefault(mi => mi.Name.Contains("دجاج")) ?? context.MenuItems.Skip(1).First();
                    var m3 = context.MenuItems.FirstOrDefault(mi => mi.Name.Contains("كباب")) ?? context.MenuItems.Skip(2).First();
                    var m4 = context.MenuItems.FirstOrDefault(mi => mi.Name.Contains("كولا")) ?? context.MenuItems.Last();

                    context.OrderItems.AddRange(
                        new OrderItem { OrderId = order1.Id, MenuItemId = m1.Id, Quantity = 1, SubTotal = m1.Price },
                        new OrderItem { OrderId = order1.Id, MenuItemId = m3.Id, Quantity = 1, SubTotal = m3.Price },
                        new OrderItem { OrderId = order2.Id, MenuItemId = m2.Id, Quantity = 1, SubTotal = m2.Price },
                        new OrderItem { OrderId = order2.Id, MenuItemId = m4.Id, Quantity = 1, SubTotal = m4.Price }
                    );
                    context.SaveChanges();
                }

                // ---- Payments ----
                if (!context.Payments.Any())
                {
                    var ord1 = context.Orders.OrderBy(o => o.Id).First();
                    var ord2 = context.Orders.OrderByDescending(o => o.Id).First();

                    context.Payments.AddRange(
                        new Payment { OrderId = ord1.Id, Amount = ord1.TotalAmount, Method = "Cash", PaymentDate = new DateTime(2024, 01, 15), Status = "Paid" },
                        new Payment { OrderId = ord2.Id, Amount = ord2.TotalAmount, Method = "Card", PaymentDate = new DateTime(2024, 02, 10), Status = "Pending" }
                    );
                    context.SaveChanges();
                }

                tx.Commit();
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }
    }
}
