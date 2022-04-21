using Application.Abstractions;
using Application.EventSourcing.EsFramework;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Events;
using MediatR;

namespace Application.ShippingOrders.Commands
{
    public class UpdateShippingOrderCommand : IRequest<ShippingOrder>
    {
        public UpdateShippingOrderCommand(Guid id, string address, string description)
        {
            Id = id;
            Address = address;
            Description = description;
        }

        public Guid Id { get; }
        public string Address { get; }
        public string Description { get; }
    }

    public class UpdateShippingOrderCommandHandler : IRequestHandler<UpdateShippingOrderCommand, ShippingOrder>
    {
        private readonly IShippingOrderRepository shippingOrderRepository;
        private readonly IAggregateRoot<ShippingOrder> aggregateRoot;
        private readonly IMessagePublisher messagePublisher;

        public UpdateShippingOrderCommandHandler(
            IShippingOrderRepository shippingOrderRepository,
            IAggregateRoot<ShippingOrder> aggregateRoot,
            IMessagePublisher messagePublisher)
        {
            this.shippingOrderRepository = shippingOrderRepository;
            this.aggregateRoot = aggregateRoot;
            this.messagePublisher = messagePublisher;
        }

        public async Task<ShippingOrder> Handle(UpdateShippingOrderCommand request, CancellationToken cancellationToken)
        {
            var previousEvents = await shippingOrderRepository
                .GetEventsByStreamIdAsync(request.Id, cancellationToken);

            if (previousEvents is null || !previousEvents.Any())
            {
                return null;
            }

            await aggregateRoot.Apply(previousEvents);

            var eventVersion = aggregateRoot.ChangeHistory.Last().Version + 1;
            var @event = new ShippingOrderUpdated(request.Address, request.Description, eventVersion);
            await aggregateRoot.Apply(new List<IEvent>() { @event });

            await shippingOrderRepository.AddEventToStreamAsync(request.Id, @event, cancellationToken);

            await messagePublisher.PublishAsync(request.Id, @event, cancellationToken);

            return aggregateRoot.CurrentState;
        }
    }
}
