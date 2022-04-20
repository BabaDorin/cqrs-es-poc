using Infrastructure.Abstractions;
using Presentation;

namespace Infrastructure.Services
{
    public class ShippingQueryServiceClient : IShippingQueryServiceClient
    {
        private readonly ShippingQueryService.ShippingQueryServiceClient client;

        public ShippingQueryServiceClient(ShippingQueryService.ShippingQueryServiceClient client)
        {
            this.client = client;
        }

        public Task PublishMessageSimulation(Models.ShippingOrderEventMessage message)
        {
            var eventMessage = new ShippingOrderEventMessage { StreamId = message.StreamId.ToString() };
            eventMessage.Events.Add(new EventMessage() { EventType = message.EventType, Data = message.Data });
            
            client.PublishMessageSimulation(eventMessage);

            return Task.CompletedTask;
        }
    }
}
