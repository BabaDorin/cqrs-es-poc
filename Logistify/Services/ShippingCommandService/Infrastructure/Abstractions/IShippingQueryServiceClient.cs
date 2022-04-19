using Infrastructure.Models;

namespace Infrastructure.Abstractions
{
    public interface IShippingQueryServiceClient
    {
        Task PublishMessageSimulation(ShippingOrderEventMessage message);
    }
}
