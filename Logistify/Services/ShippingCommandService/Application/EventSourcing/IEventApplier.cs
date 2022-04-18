using Domain.Abstractions;

namespace Application.Event_Handling
{
    public interface IEventApplier<TEvent, TEntity> 
        where TEvent : IEvent 
        where TEntity : class
    {
        Task Apply(TEvent @event, TEntity entity);
    }
}
