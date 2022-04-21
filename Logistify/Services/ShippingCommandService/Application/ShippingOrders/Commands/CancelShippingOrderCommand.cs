using Application.Abstractions;
using Application.EventSourcing.EsFramework;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;
using MediatR;

namespace Application.ShippingOrders.Commands
{
    public class CancelShippingOrderCommand : IRequest<bool>
    {
        public CancelShippingOrderCommand(Guid orderId)
        {
            OrderId = orderId;
        }

        public Guid OrderId { get; }
    }

    public class CancelShippingOrderCommandHandler : IRequestHandler<CancelShippingOrderCommand, bool>
    {
        private readonly IAggregateRoot<ShippingOrder> aggregateRoot;
        private readonly IShippingOrderRepository shippingOrderRepository;
        private readonly IMessagePublisher messagePublisher;

        public CancelShippingOrderCommandHandler(
            IAggregateRoot<ShippingOrder> aggregateRoot,
            IShippingOrderRepository shippingOrderRepository,
            IMessagePublisher messagePublisher)
        {
            this.aggregateRoot = aggregateRoot;
            this.shippingOrderRepository = shippingOrderRepository;
            this.messagePublisher = messagePublisher;
        }

        public async Task<bool> Handle(CancelShippingOrderCommand request, CancellationToken cancellationToken)
        {
            var previousEvents = await shippingOrderRepository
                .GetEventsByStreamIdAsync(request.OrderId, cancellationToken);

            if (previousEvents is null || !previousEvents.Any())
            {
                return false;
            }

            await aggregateRoot.Apply(previousEvents);

            if (aggregateRoot.CurrentState.Status == OrderStatus.Canceled)
            {
                return false;
            }

            var eventVersion = aggregateRoot.ChangeHistory.Last().Version + 1;
            var @event = new ShippingOrderCanceled(eventVersion);

            await aggregateRoot.Apply(new List<IEvent> { @event });

            await shippingOrderRepository.AddEventToStreamAsync(aggregateRoot.CurrentState.Id, @event, cancellationToken);

            await messagePublisher.PublishAsync(aggregateRoot.CurrentState.Id, @event, cancellationToken);

            return true;
        }
    }
}
