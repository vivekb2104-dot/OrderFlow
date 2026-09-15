using OrderFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace OrderFlow.Infrastructure.Persistence
{
    public class OrderFlowDbContext : DbContext
    {
        public OrderFlowDbContext(DbContextOptions<OrderFlowDbContext> options) : base(options) { }

        public DbSet<Order> Orders => Set<Order>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(builder =>
            {
                builder.HasKey(o => o.Id);

                builder.OwnsMany(o => o.Lines, lb =>
                {
                    lb.WithOwner().HasForeignKey("OrderId");
                    lb.Property<int>("Id");
                    lb.HasKey("Id");

                    lb.Property(l => l.ProductId).IsRequired();
                    lb.Property(l => l.Quantity).IsRequired();
                    lb.Property(l => l.UnitPrice).IsRequired();
                });

                builder.Navigation(o => o.Lines).UsePropertyAccessMode(PropertyAccessMode.Field);
            });
        }
    }
}
