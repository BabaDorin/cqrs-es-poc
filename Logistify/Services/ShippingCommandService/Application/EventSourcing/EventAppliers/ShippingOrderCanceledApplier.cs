using Application.EventSourcing.EsFramework;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;

namespace Application.EventSourcing.EventAppliers
{
    public class ShippingOrderCanceledApplier : IEventApplier<ShippingOrderCanceled, ShippingOrder>
    {
        public Task Apply(ShippingOrderCanceled @event, ShippingOrder entity)
        {
            entity.Status = OrderStatus.Canceled;

            return Task.CompletedTask;
        }
    }
}
