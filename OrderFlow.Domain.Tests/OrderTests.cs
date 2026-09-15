using OrderFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderFlow.Domain.Tests
{
    public class OrderTests
    {
        [Fact]
        public void Confirm_WithNoLines_Throws()
        {
            var order = Order.Create("cust-1");
            Assert.Throws<InvalidOperationException>(() => order.Confirm());
        }
        [Fact]
        public void AddLine_ThenConfirm_Succeeds()
        {
            var order = Order.Create("cust-1");
            order.AddLine("prod-1", 2, 9.99m);
            order.Confirm();
            Assert.Equal(OrderStatus.Confirmed, order.Status);
        }
    }
}
