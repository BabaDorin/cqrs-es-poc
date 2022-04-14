using Application.Models;

namespace Application.Interfaces
{
    public interface IShippingOrderQueryClient
    {
        Task<IList<ShippingOrderDto>> GetAsync(CancellationToken cancellationToken);
        Task<ShippingOrderDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
