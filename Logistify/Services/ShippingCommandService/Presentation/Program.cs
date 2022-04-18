using Application.EventSourcing;
using Application.ShippingOrders.Commands;
using Presentation.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddEventResolver(typeof(CancelShippingOrderCommand).Assembly);

var app = builder.Build();

app.MapGrpcService<ShippingOrderCommandService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client.");

app.Run();
