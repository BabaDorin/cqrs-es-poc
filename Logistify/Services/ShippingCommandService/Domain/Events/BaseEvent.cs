using Domain.Abstractions;

namespace Domain.Events
{
    public abstract class BaseEvent : IEvent
    {
        public BaseEvent(int version)
        {
            Version = version;
        }

        public int Version { get; set; }
    }
}
