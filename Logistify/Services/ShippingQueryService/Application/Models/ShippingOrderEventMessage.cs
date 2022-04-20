namespace Application.Models
{
    public class EventMessage
    {
        public EventMessage(string eventType, string data)
        {
            EventType = eventType;
            Data = data;
        }

        public string EventType { get; }
        public string Data { get; }
    }

    public class ShippingOrderEventMessage
    {
        public ShippingOrderEventMessage(Guid streamId, IList<EventMessage> events)
        {
            StreamId = streamId;
            Events = events;
        }

        public Guid StreamId { get; }

        public IList<EventMessage> Events { get; }
    }
}
