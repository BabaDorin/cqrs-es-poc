using Application.EventSourcing.EsFramework;
using Domain.Entities;
using Domain.Events;

namespace Application.EventSourcing.EventAppliers
{
    public class ShippingOrderUpdatedToOrderApplier : IEventApplier<ShippingOrderUpdated, ShippingOrder>
    {
        public Task Apply(ShippingOrderUpdated @event, ShippingOrder entity)
        {
            entity.Address = @event.Address;

            return Task.CompletedTask;
        }
    }

    public class ShippingOrderUpdatedToOrderDetailsApplier : IEventApplier<ShippingOrderUpdated, ShippingOrderDetails>
    {
        public Task Apply(ShippingOrderUpdated @event, ShippingOrderDetails entity)
        {
            entity.Address = @event.Address;
            entity.Description = @event.Description;

            return Task.CompletedTask;
        }
    }
}
