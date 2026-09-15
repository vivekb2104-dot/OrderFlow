using System;
using System.Collections.Generic;
using System.Text;

namespace OrderFlow.Domain.Entities
{
    public class OrderLine
    {
        public string ProductId { get; }
        public int Quantity { get; }
        public decimal UnitPrice { get; }
        public OrderLine(string productId, int quantity, decimal unitPrice)
        {
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
}
