using Domain.Abstractions;

namespace Application.EventSourcing.EsFramework
{
    public interface IEventApplier<TEvent, TEntity>
        where TEvent : IEvent
        where TEntity : class
    {
        Task Apply(TEvent @event, TEntity entity);
    }
}
