using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderFlow.API.Contracts;
using OrderFlow.Application.Orders;
using OrderFlow.Domain.Interfaces;

namespace OrderFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly PlaceOrderHandler _handler;
        public OrdersController(PlaceOrderHandler handler) => _handler = handler;
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
        {
            var command = new PlaceOrderCommand(
            request.CustomerId,
            request.Lines.Select(l => new OrderLineInput(l.ProductId, l.Quantity, l.UnitPrice)).ToList());
            var orderId = await _handler.HandleAsync(command);
            return CreatedAtAction(nameof(GetById), new { id = orderId }, new { id = orderId });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, [FromServices] IOrderRepository repo)
        {
            var order = await repo.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }
    }
}
