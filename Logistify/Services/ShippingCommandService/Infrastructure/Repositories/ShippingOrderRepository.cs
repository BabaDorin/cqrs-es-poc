using Application.Abstractions;
using Domain.Abstractions;

namespace Infrastructure.Repositories
{
    public class ShippingOrderRepository : IShippingOrderRepository
    {
        private readonly Dictionary<Guid, IList<IEvent>> eventStreams;

        public ShippingOrderRepository()
        {
            eventStreams = new Dictionary<Guid, IList<IEvent>>();
        }

        public Task<bool> AddEventToStreamAsync(Guid streamId, IEvent @event, CancellationToken cancellationToken)
        {
            if(eventStreams.TryGetValue(streamId, out IList<IEvent>? stream))
            {
                stream.Add(@event);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public Task<IList<IEvent>> GetEventsByAggregateIdAsync(Guid id, CancellationToken cancellationToken)
        {
            if (eventStreams.TryGetValue(id, out IList<IEvent>? stream))
            {
                return Task.FromResult(stream);
            }

            return null;
        }
    }
}
