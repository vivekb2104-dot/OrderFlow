using OrderFlow.Domain.Entities;
using OrderFlow.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderFlow.Application.Orders
{
    public record PlaceOrderCommand(string CustomerId, List<OrderLineInput> Lines);
    public record OrderLineInput(string ProductId, int Quantity, decimal UnitPrice);
    public class PlaceOrderHandler
    {
        private readonly IOrderRepository _repository;
        public PlaceOrderHandler(IOrderRepository repository) => _repository = repository;
        public async Task<Guid> HandleAsync(PlaceOrderCommand command)
        {
            var order = Order.Create(command.CustomerId);
            foreach (var line in command.Lines)
                order.AddLine(line.ProductId, line.Quantity, line.UnitPrice);
            order.Confirm();
            await _repository.AddAsync(order);
            await _repository.SaveChangesAsync();
            return order.Id;
        }
    }
}
