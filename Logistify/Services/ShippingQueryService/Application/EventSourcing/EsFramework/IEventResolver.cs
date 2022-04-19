namespace Application.EventSourcing.EsFramework
{
    public interface IEventResolver
    {
        Task Apply<TEvent, TEntity>(TEvent @event, TEntity entity)
            where TEvent : IEvent
            where TEntity : class;
    }
}
