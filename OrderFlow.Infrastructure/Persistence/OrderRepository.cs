using OrderFlow.Domain.Entities;
using OrderFlow.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace OrderFlow.Infrastructure.Persistence
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderFlowDbContext _context;

        public OrderRepository(OrderFlowDbContext context) => _context = context;

        public async Task<Order?> GetByIdAsync(Guid id) =>
            await _context.Orders.Include(o => o.Lines).FirstOrDefaultAsync(o => o.Id == id);

        public async Task AddAsync(Order order) => await _context.Orders.AddAsync(order);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
