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
