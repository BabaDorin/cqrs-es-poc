using Application.Models;

namespace Application.Interfaces
{
    public interface IShippingOrderCommandClient
    {
        Task<ShippingOrderDetailsDto> CreateShippingOrderAsync(
            ShippingOrderDetailsDto orderDetails, CancellationToken cancellationToken);

        Task<ShippingOrderDetailsDto> UpdateShippingOrderAsync(
            Guid id, ShippingOrderDetailsDto orderDetails, CancellationToken cancellationToken);
    }
}
