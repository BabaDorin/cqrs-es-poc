using Application.Events;
using Application.EventSourcing.EventAppliers;
using System.Reflection;

namespace Application.EventSourcing.EsFramework
{
    public class EventResolver : IEventResolver
    {
        private readonly Dictionary<Tuple<Type, Type>, object> eventAppliers;

        public EventResolver(params Assembly[] assemblies)
        {
            eventAppliers = new Dictionary<Tuple<Type, Type>, object>();

            foreach (var assembly in assemblies)
            {
                var assemblyAppliers = assembly.GetTypes()
                    .Where(x => x.IsClass 
                        && x.GetInterfaces()
                            .Any(i => i.Name == typeof(IEventApplier<,>).Name && i.Assembly == typeof(IEventApplier<,>).Assembly));

                assemblyAppliers
                    .ToList()
                    .ForEach(applier =>
                    {
                        var key = new Tuple<Type, Type>(
                            applier.GetInterface(typeof(IEventApplier<,>).Name).GenericTypeArguments[0],
                            applier.GetInterface(typeof(IEventApplier<,>).Name).GenericTypeArguments[1]);
                        var value = Activator.CreateInstance(applier);

                        if (value != null) eventAppliers.Add(key, value);
                    });
            }
        }

        public Task Apply<TEvent, TEntity>(TEvent @event, TEntity entity)
            where TEvent : IEvent
            where TEntity : class
        {
            if (!eventAppliers.TryGetValue(new Tuple<Type, Type>(@event.GetType(), entity.GetType()), out object? eventApplier))
            {
                if (eventApplier is null || !eventApplier.GetType().IsAssignableFrom(typeof(IEventApplier<TEvent, TEntity>)))
                {
                    throw new InvalidOperationException("No event applier for found for the specified TEvent and TEntity pair.");
                }

                return (eventApplier as IEventApplier<TEvent, TEntity>)!.Apply(@event, entity);
            }

            throw new InvalidOperationException("No event applier for found for the specified TEvent and TEntity pair.");
        }
    }
}
