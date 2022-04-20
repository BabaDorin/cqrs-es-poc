using Application.EventSourcing.EsFramework;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;

namespace Application.EventSourcing.EventAppliers
{
    public class ShippingOrderCreatedApplier : IEventApplier<ShippingOrderCreated, ShippingOrder>
    {
        public Task Apply(ShippingOrderCreated @event, ShippingOrder entity)
        {
            entity.Id = @event.Id;
            entity.Address = @event.Address;
            entity.PlacedBy = @event.PlacedBy;
            entity.Description = @event.Description;
            entity.Status = OrderStatus.Pending;

            return Task.CompletedTask;
        }
    }
}
