namespace Infrastructure.Models
{
    public class ShippingOrderEventMessage
    {
        public ShippingOrderEventMessage(Guid streamId, string eventType, string data)
        {
            StreamId = streamId;
            EventType = eventType;
            Data = data;
        }

        public Guid StreamId { get; }
        public string EventType { get; }
        public string Data { get; }
    }
}
