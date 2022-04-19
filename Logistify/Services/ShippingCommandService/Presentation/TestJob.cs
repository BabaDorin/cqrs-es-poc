using Infrastructure.Abstractions;

namespace Presentation
{
    public class TestJob : IHostedService
    {
        private readonly IShippingQueryServiceClient queryServiceClient;

        public TestJob(IShippingQueryServiceClient queryServiceClient)
        {
            this.queryServiceClient = queryServiceClient;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await queryServiceClient.PublishMessageSimulation(new Infrastructure.Models.ShippingOrderEventMessage(Guid.NewGuid(), "type", "data"));

            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
