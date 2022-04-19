using Application.Abstractions;
using Application.EventSourcing.EsFramework;
using Application.ShippingOrders.Commands;
using Domain.Entities;
using Infrastructure.Extensions;
using Infrastructure.Repositories;
using Infrastructure.Services;
using MediatR;
using Presentation;
using Presentation.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(typeof(CancelShippingOrderCommand).Assembly);

builder.Services.AddSingleton<IShippingOrderRepository, ShippingOrderRepository>();
builder.Services.AddSingleton<IMessagePublisher, MessagePublisherSimulator>();

builder.Services.AddGrpc();
builder.Services.AddGrpcClients(builder.Configuration);

builder.Services.AddEventResolver(typeof(CancelShippingOrderCommand).Assembly);
builder.Services.AddTransient<IAggregateRoot<ShippingOrder>, AggregateRoot<ShippingOrder>>();

builder.Services.AddHostedService<TestJob>();

var app = builder.Build();

app.MapGrpcService<ShippingOrderCommandService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client.");

app.Run();
