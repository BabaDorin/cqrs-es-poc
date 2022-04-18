using Application.EventSourcing.EsFramework;
using Domain.Entities;
using Domain.Events;

namespace Application.EventSourcing.EventAppliers
{
    public class ShippingOrderUpdatedApplier : IEventApplier<ShippingOrderCreated, ShippingOrder>
    {
        public Task Apply(ShippingOrderCreated @event, ShippingOrder entity)
        {
            entity.Address = @event.Address;
            entity.Description = @event.Description;

            return Task.CompletedTask;
        }
    }
}
