using Azure.Identity;
using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;
using OrderFlow.Application.Orders;
using OrderFlow.Domain.Interfaces;
using OrderFlow.Infrastructure.Messaging;
using OrderFlow.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

if (!builder.Environment.IsDevelopment() || builder.Configuration.GetValue<bool>("UseKeyVaultLocally"))
{
    builder.Configuration.AddAzureKeyVault(
    new Uri("https://kv-orderflow-vivek.vault.azure.net/"),
    new DefaultAzureCredential());
}
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<PlaceOrderHandler>();
builder.Services.AddDbContext<OrderFlowDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("OrderFlowDb")));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddSingleton(new ServiceBusClient("order-confirmed.servicebus.windows.net", new DefaultAzureCredential()));
builder.Services.AddScoped<IOrderConfirmedPublisher, OrderConfirmedPublisher>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
