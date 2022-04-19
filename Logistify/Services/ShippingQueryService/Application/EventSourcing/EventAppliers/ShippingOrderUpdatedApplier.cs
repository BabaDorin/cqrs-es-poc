using Application.EventSourcing.EsFramework;
using Domain.Entities;
using Domain.Events;

namespace Application.EventSourcing.EventAppliers
{
    public class ShippingOrderUpdatedToOrderApplier : IEventApplier<ShippingOrderCreated, ShippingOrder>
    {
        public Task Apply(ShippingOrderCreated @event, ShippingOrder entity)
        {
            entity.Address = @event.Address;

            return Task.CompletedTask;
        }
    }

    public class ShippingOrderUpdatedToOrderDetailsApplier : IEventApplier<ShippingOrderCreated, ShippingOrderDetails>
    {
        public Task Apply(ShippingOrderCreated @event, ShippingOrderDetails entity)
        {
            entity.Address = @event.Address;
            entity.Description = @event.Description;

            return Task.CompletedTask;
        }
    }
}
