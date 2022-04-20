using Application.Abstractions;
using Domain.Abstractions;
using Infrastructure.Abstractions;
using Infrastructure.Models;
using System.Text.Json;

namespace Infrastructure.Services
{
    /// <summary>
    /// This is a simulator. In a real life scenario messages have to be published to some sort of service bus
    /// (RabbitMQ, Azure Service bus etc).
    /// </summary>
    public class MessagePublisherSimulator : IMessagePublisher
    {
        private readonly IShippingQueryServiceClient shippingQueryService;

        public MessagePublisherSimulator(IShippingQueryServiceClient shippingQueryService)
        {
            this.shippingQueryService = shippingQueryService;
        }

        public Task PublishAsync(Guid streamId, IEvent @event, CancellationToken cancellationToken)
        {
            var json = JsonSerializer.Serialize(@event, @event.GetType());

            var message = new ShippingOrderEventMessage(streamId, @event.GetType().Name, JsonSerializer.Serialize(@event, @event.GetType()));
            shippingQueryService.PublishMessageSimulation(message);

            return Task.CompletedTask;
        }
    }
}
