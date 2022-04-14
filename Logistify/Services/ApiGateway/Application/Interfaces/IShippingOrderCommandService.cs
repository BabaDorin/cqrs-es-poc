using Application.Models;

namespace Application.Interfaces
{
    public interface IShippingOrderCommandService
    {
        Task<ShippingOrderDetailsDto> CreateShippingOrderAsync(
            ShippingOrderDetailsDto orderDetails, CancellationToken cancellationToken);

        Task<ShippingOrderDetailsDto> UpdateShippingOrderAsync(
            Guid id, ShippingOrderDetailsDto orderDetails, CancellationToken cancellationToken);
    }
}
