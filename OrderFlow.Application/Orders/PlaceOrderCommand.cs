using OrderFlow.Domain.Entities;
using OrderFlow.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderFlow.Application.Orders
{
    public record PlaceOrderCommand(string CustomerId, List<OrderLineInput> Lines);
    public record OrderLineInput(string ProductId, int Quantity, decimal UnitPrice);

    public interface IOrderConfirmedPublisher
    {
        Task PublishAsync(Guid orderId, string customerId);
    }

    public class PlaceOrderHandler
    {
        private readonly IOrderRepository _repository;
        private readonly IOrderConfirmedPublisher _publisher;
        public PlaceOrderHandler(IOrderRepository repository, IOrderConfirmedPublisher publisher)
        {
            _repository = repository;
            _publisher = publisher;
        }
        public async Task<Guid> HandleAsync(PlaceOrderCommand command)
        {
            var order = Order.Create(command.CustomerId);
            foreach (var line in command.Lines)
                order.AddLine(line.ProductId, line.Quantity, line.UnitPrice);
            order.Confirm();
            await _repository.AddAsync(order);
            await _repository.SaveChangesAsync();
            await _publisher.PublishAsync(order.Id, order.CustomerId);
            return order.Id;
        }
    }
}
