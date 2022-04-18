using Application.Abstractions;
using Application.EventSourcing.EsFramework;
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

        public CancelShippingOrderCommandHandler(
            IAggregateRoot<ShippingOrder> aggregateRoot,
            IShippingOrderRepository shippingOrderRepository)
        {
            this.aggregateRoot = aggregateRoot;
            this.shippingOrderRepository = shippingOrderRepository;
        }

        public async Task<bool> Handle(CancelShippingOrderCommand request, CancellationToken cancellationToken)
        {
            var previousEvents = await shippingOrderRepository
                .GetEventsByAggregateIdAsync(request.OrderId, cancellationToken);

            if (previousEvents is null || !previousEvents.Any())
            {
                return false;
            }

            await aggregateRoot.Apply(previousEvents);

            if (aggregateRoot.CurrentState.Status == OrderStatus.Canceled)
            {
                return false;
            }

            aggregateRoot.CurrentState.Status = OrderStatus.Canceled;

            var @event = new ShippingOrderCanceled();
            await shippingOrderRepository.AddEventToStreamAsync(aggregateRoot.CurrentState.Id, @event, cancellationToken);

            return true;
        }
    }
}
