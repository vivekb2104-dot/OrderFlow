using System;
using System.Collections.Generic;
using System.Text;

namespace OrderFlow.Domain.Entities
{
    public enum OrderStatus { Pending, Confirmed, Shipped, Cancelled }
    public class Order
    {
        private readonly List<OrderLine> _lines = new();
        public Guid Id { get; private set; }
        public string CustomerId { get; private set; }
        public OrderStatus Status { get; private set; }
        public IReadOnlyCollection<OrderLine> Lines => _lines.AsReadOnly();
        public decimal Total => _lines.Sum(l => l.Quantity * l.UnitPrice);
        private Order() { } // for EF Core
        public static Order Create(string customerId)
        {
            if (string.IsNullOrWhiteSpace(customerId))
                throw new ArgumentException("Customer id is required.");
            return new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                Status = OrderStatus.Pending
            };
        }
        public void AddLine(string productId, int quantity, decimal unitPrice)
        {
            if (Status != OrderStatus.Pending)
                throw new InvalidOperationException("Cannot modify a confirmed order.");
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be positive.");
            _lines.Add(new OrderLine(productId, quantity, unitPrice));
        }
        public void Confirm()
        {
            if (!_lines.Any())
                throw new InvalidOperationException("Cannot confirm an order with no lines.");
            Status = OrderStatus.Confirmed;
        }
    }
}
