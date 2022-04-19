using Application.EventSourcing.EsFramework;
using Application.Interfaces;
using Application.ShippingOrders.Commands;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Services.AddEventResolver(typeof(HandleShippingOrderEventMessageCommand).Assembly);
builder.Services.AddSingleton<IShippingOrdersRespository, ShippingOrderRepository>();

var app = builder.Build();

app.MapGrpcService<ShippingOrderQueryService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client.");

app.Run();