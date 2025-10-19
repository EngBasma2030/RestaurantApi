using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Quantity)
                .IsRequired();

            builder.Property(o => o.SubTotal)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasOne(o => o.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.MenuItem)
                .WithMany()
                .HasForeignKey(o => o.MenuItemId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
