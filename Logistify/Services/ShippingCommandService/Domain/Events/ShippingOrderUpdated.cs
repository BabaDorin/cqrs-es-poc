using Domain.Abstractions;

namespace Domain.Events
{
    public class ShippingOrderUpdated : IEvent
    {
        public ShippingOrderUpdated(string address, string description)
        {
            Address = address;
            Description = description;
        }

        public string Address { get; }
        public string Description { get; }
    }
}
