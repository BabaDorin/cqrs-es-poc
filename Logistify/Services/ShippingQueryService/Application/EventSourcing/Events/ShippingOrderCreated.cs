using Application.EventSourcing.EsFramework;
using Domain.Enums;

namespace Domain.Events
{
    public class ShippingOrderCreated : IEvent
    {
        public ShippingOrderCreated(Guid id, OrderStatus status, string address, string description, string placedBy)
        {
            Id = id;
            Status = status;
            Address = address;
            Description = description;
            PlacedBy = placedBy;
        }

        public Guid Id { get; }
        public OrderStatus Status { get; set; }
        public string Address { get; }
        public string Description { get; }
        public string PlacedBy { get; }
    }
}
