using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using OrderFlow.Application.Orders;

namespace OrderFlow.Infrastructure.Messaging
{
    public class OrderConfirmedPublisher : IOrderConfirmedPublisher
    {
        private readonly ServiceBusSender _sender;
        public OrderConfirmedPublisher(ServiceBusClient client) =>
        _sender = client.CreateSender("order-confirmed");
        public async Task PublishAsync(Guid orderId, string customerId)
        {
            var payload = JsonSerializer.Serialize(new { orderId, customerId });
            var message = new ServiceBusMessage(payload);
            await _sender.SendMessageAsync(message);
        }
    }
}
