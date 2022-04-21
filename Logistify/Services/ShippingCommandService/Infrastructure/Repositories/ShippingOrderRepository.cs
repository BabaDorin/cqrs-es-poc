using Application.Abstractions;
using Domain.Abstractions;

namespace Infrastructure.Repositories
{
    public class ShippingOrderRepository : IShippingOrderRepository
    {
        private static readonly Dictionary<Guid, IList<IEvent>> eventStreams = new();

        public Task<bool> AddEventToStreamAsync(Guid streamId, IEvent @event, CancellationToken cancellationToken)
        {
            if(eventStreams.TryGetValue(streamId, out IList<IEvent> stream))
            {
                if (stream.Any(e => e.Version == @event.Version))
                {
                    // Handle conflicts (2 events with the same version).
                    var conflictingEvent = stream.First(e => e.Version == @event.Version);
                    
                    if (conflictingEvent.GetType() == @event.GetType())
                    {
                        throw new InvalidOperationException("Could not register the event due to concurrency issues." +
                            "An event with the same version and type was already registered. " +
                            "Please, try again.");
                    }
                    else
                    {
                        @event.Version = conflictingEvent.Version + 1;
                    }
                }

                stream.Add(@event);
                return Task.FromResult(true);
            }
            else
            {
                eventStreams.Add(streamId, new List<IEvent>() { @event });
            }

            return Task.FromResult(false);
        }

        public Task<IList<IEvent>> GetEventsByStreamIdAsync(Guid streamId, CancellationToken cancellationToken)
        {
            if (eventStreams.TryGetValue(streamId, out IList<IEvent> stream))
            {
                return Task.FromResult(stream);
            }

            return null;
        }
    }
}
