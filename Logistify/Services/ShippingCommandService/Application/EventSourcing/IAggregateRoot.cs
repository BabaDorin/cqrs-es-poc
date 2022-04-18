using Domain.Abstractions;

namespace Application.EventSourcing
{
    public interface IAggregateRoot<TEntity> where TEntity : class
    {
        TEntity CurrentState { get; }
        IList<TEntity> ChangeHistory { get; }

        Task Apply<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
