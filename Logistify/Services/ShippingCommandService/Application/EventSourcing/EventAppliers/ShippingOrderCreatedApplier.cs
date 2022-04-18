using Application.EventSourcing.EsFramework;
using Domain.Entities;
using Domain.Events;

namespace Application.EventSourcing.EventAppliers
{
    public class ShippingOrderCreatedApplier : IEventApplier<ShippingOrderCreated, ShippingOrder>
    {
        public Task Apply(ShippingOrderCreated @event, ShippingOrder entity)
        {
            entity.Address = @event.Address;
            entity.PlacedBy = @event.PlacedBy;
            entity.Description = @event.Description;

            return Task.CompletedTask;
        }
    }
}
