using Domain.Abstractions;

namespace Application.EventSourcing.EsFramework
{
    public interface IAggregateRoot<TEntity> where TEntity : class
    {
        TEntity CurrentState { get; }
        IList<IEvent> ChangeHistory { get; }

        Task Apply(IList<IEvent> events);
    }
}
