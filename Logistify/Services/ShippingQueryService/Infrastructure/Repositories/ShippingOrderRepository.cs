using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class ShippingOrderRepository : IShippingOrdersRespository
    {
        private readonly IList<ShippingOrder> shippingOrders;
        private readonly IList<ShippingOrderDetails> shippingOrderDetails;

        public ShippingOrderRepository()
        {
            shippingOrders = new List<ShippingOrder>();
            shippingOrderDetails = new List<ShippingOrderDetails>();
        }

        public Task<IList<ShippingOrder>> GetAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(shippingOrders);
        }

        public Task<ShippingOrder> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Task.FromResult(shippingOrders.FirstOrDefault(x => x.Id == id));
        }

        public Task<ShippingOrderDetails> GetDetailsByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return Task.FromResult(shippingOrderDetails.FirstOrDefault(x => x.Id == id));
        }

        public Task<ShippingOrder> InsertAsync(ShippingOrder shippingOrder, CancellationToken cancellationToken)
        {
            shippingOrders.Add(shippingOrder);
            
            return Task.FromResult(shippingOrder);
        }

        public Task<ShippingOrderDetails> InsertAsync(ShippingOrderDetails shippingOrder, CancellationToken cancellationToken)
        {
            shippingOrderDetails.Add(shippingOrder);

            return Task.FromResult(shippingOrder);
        }

        public Task UpdateAsync(ShippingOrder order, CancellationToken cancellationToken)
        {
            shippingOrders.Remove(shippingOrders.First(o => o.Id == order.Id));
            shippingOrders.Add(order);

            return Task.FromResult(order);
        }

        public Task UpdateAsync(ShippingOrderDetails orderDetails, CancellationToken cancellationToken)
        {
            shippingOrderDetails.Remove(shippingOrderDetails.First(o => o.Id == orderDetails.Id));
            shippingOrderDetails.Add(orderDetails);

            return Task.FromResult(orderDetails);
        }
    }
}