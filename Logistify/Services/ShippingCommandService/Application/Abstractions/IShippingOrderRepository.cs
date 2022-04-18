using Domain.Abstractions;

namespace Application.Abstractions
{
    public interface IShippingOrderRepository
    {
        Task<IList<IEvent>> GetEventsByAggregateIdAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> AddEventToStreamAsync(Guid streamId, IEvent @event, CancellationToken cancellationToken);
    }
}
