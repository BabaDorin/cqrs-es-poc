using Application.Events;
using Application.EventSourcing.EsFramework;
using Domain.Entities;
using Domain.Enums;

namespace Application.EventSourcing.EventAppliers
{
    public class ShippingOrderCanceledToOrderApplier : IEventApplier<ShippingOrderCanceled, ShippingOrder>
    {
        public Task Apply(ShippingOrderCanceled @event, ShippingOrder entity)
        {
            entity.Status = OrderStatus.Canceled;

            return Task.CompletedTask;
        }
    }

    public class ShippingOrderCanceledToOrderDetailsApplier : IEventApplier<ShippingOrderCanceled, ShippingOrderDetails>
    {
        public Task Apply(ShippingOrderCanceled @event, ShippingOrderDetails entity)
        {
            entity.Status = OrderStatus.Canceled;

            return Task.CompletedTask;
        }
    }
}
