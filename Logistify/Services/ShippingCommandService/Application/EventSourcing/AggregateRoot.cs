using Domain.Abstractions;

namespace Application.EventSourcing
{
    public class AggregateRoot<TEntity> : IAggregateRoot<TEntity> where TEntity : class
    {
        private readonly IEventResolver eventResolver;

        public AggregateRoot(IEventResolver eventResolver)
        {
            CurrentState = Activator.CreateInstance<TEntity>();
            ChangeHistory = new List<TEntity>();
            this.eventResolver = eventResolver;
        }
        
        public TEntity CurrentState { get; }

        public IList<TEntity> ChangeHistory { get; }

        public Task Apply<TEvent>(TEvent @event)
            where TEvent : IEvent
        {
            return eventResolver.Apply(@event, CurrentState);
        }
    }
}
