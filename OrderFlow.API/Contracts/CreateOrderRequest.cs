namespace OrderFlow.API.Contracts
{
    public record CreateOrderRequest(string CustomerId, List<OrderLineDto> Lines);
    public record OrderLineDto(string ProductId, int Quantity, decimal UnitPrice);
}
