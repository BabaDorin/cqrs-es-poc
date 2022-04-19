using Application.EventSourcing.EsFramework;
using Domain.Entities;
using Domain.Events;

namespace Application.EventSourcing.EventAppliers
{
    public class ShippingOrderUpdatedApplier : IEventApplier<ShippingOrderUpdated, ShippingOrder>
    {
        public Task Apply(ShippingOrderUpdated @event, ShippingOrder entity)
        {
            entity.Address = @event.Address;
            entity.Description = @event.Description;

            return Task.CompletedTask;
        }
    }
}
