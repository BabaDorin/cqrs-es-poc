namespace Domain.Events
{
    public class ShippingOrderUpdated : BaseEvent
    {
        public ShippingOrderUpdated(string address, string description, int version)
            : base(version)
        {
            Address = address;
            Description = description;
        }

        public string Address { get; }
        public string Description { get; }
    }
}
