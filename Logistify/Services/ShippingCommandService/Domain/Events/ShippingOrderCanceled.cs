namespace Domain.Events
{
    public class ShippingOrderCanceled : BaseEvent
    {
        public ShippingOrderCanceled(int version)
            : base(version)
        {
        }
    }
}
