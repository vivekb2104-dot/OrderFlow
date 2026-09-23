using Microsoft.Azure.Functions.Worker;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace OrderFlow.Functions.OrderConfirmation
{
    public class OrderConfirmationFunction
    {
        [Function("OrderConfirmed")]
        public void Run(
        [ServiceBusTrigger("order-confirmed", Connection = "ServiceBusConnection")] string message,
        FunctionContext context)
        {
            var logger = context.GetLogger("OrderConfirmed");
            var data = JsonSerializer.Deserialize<OrderConfirmedMessage>(message);
            logger.LogInformation("Sending confirmation for order {OrderId}", data.orderId);
            // send email/SMS here (e.g., via Azure Communication Services or SendGrid)
        }
    }

    public record OrderConfirmedMessage(Guid orderId, string customerId);
}
