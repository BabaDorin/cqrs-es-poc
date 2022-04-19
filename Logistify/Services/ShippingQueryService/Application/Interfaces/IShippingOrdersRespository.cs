using Domain.Entities;

namespace Application.Interfaces
{
    public interface IShippingOrdersRespository
    {
        Task<ShippingOrder> InsertAsync(ShippingOrder shippingOrder, CancellationToken cancellationToken);
        Task<ShippingOrderDetails> InsertAsync(ShippingOrderDetails shippingOrder, CancellationToken cancellationToken);
        Task<IList<ShippingOrder>> GetAsync(CancellationToken cancellationToken);
        Task<ShippingOrderDetails> GetDetailsByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<ShippingOrder> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateAsync(ShippingOrder order, CancellationToken cancellationToken);
        Task UpdateAsync(ShippingOrderDetails orderDetails, CancellationToken cancellationToken);
    }
}
