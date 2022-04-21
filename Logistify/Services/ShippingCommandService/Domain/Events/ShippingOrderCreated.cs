using Domain.Enums;

namespace Domain.Events
{
    public class ShippingOrderCreated : BaseEvent
    {
        public ShippingOrderCreated(Guid id, OrderStatus status, string address, string description, string placedBy, int version)
            : base(version)
        {
            Id = id;
            Status = status;
            Address = address;
            Description = description;
            PlacedBy = placedBy;
        }

        public Guid Id { get; }
        public OrderStatus Status { get; }
        public string Address { get; }
        public string Description { get; }
        public string PlacedBy { get; }
    }
}
