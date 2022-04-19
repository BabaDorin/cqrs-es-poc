using Application.EventSourcing.EsFramework;
using Application.ShippingOrders.Commands;
using Domain.Entities;
using Infrastructure.Extensions;
using Presentation.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
builder.Services.AddGrpcClients(builder.Configuration);

builder.Services.AddEventResolver(typeof(CancelShippingOrderCommand).Assembly);
builder.Services.AddTransient<IAggregateRoot<ShippingOrder>, AggregateRoot<ShippingOrder>>();

var app = builder.Build();

app.MapGrpcService<ShippingOrderCommandService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client.");

app.Run();
