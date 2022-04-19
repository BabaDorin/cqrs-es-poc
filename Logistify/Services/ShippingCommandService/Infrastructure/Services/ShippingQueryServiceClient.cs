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
            var eventMessage = new ShippingOrderEventMessage
            {
                StreamId = message.StreamId.ToString(),
                Data = message.Data,
                EventType = message.EventType,
            };

            client.PublishMessageSimulationAsync(eventMessage, new Grpc.Core.Metadata());

            return Task.CompletedTask;
        }
    }
}
