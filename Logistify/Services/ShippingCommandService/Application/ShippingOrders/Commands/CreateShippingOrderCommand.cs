using Application.Abstractions;
using Application.EventSourcing.EsFramework;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Enums;
using Domain.Events;
using MediatR;

namespace Application.ShippingOrders.Commands
{
    public class CreateShippingOrderCommand : IRequest<ShippingOrder>
    {
        public CreateShippingOrderCommand(string address, string description, string placedBy)
        {
            Address = address;
            Description = description;
            PlacedBy = placedBy;
        }

        public string Address { get; }
        public string Description { get; }
        public string PlacedBy { get; }
    }

    public class CreateShippingOrderCommandHandler : IRequestHandler<CreateShippingOrderCommand, ShippingOrder>
    {
        private readonly IShippingOrderRepository shippingOrderRepository;
        private readonly IAggregateRoot<ShippingOrder> aggregateRoot;
        private readonly IMessagePublisher messagePublisher;

        public CreateShippingOrderCommandHandler(
            IShippingOrderRepository shippingOrderRepository,
            IAggregateRoot<ShippingOrder> aggregateRoot,
            IMessagePublisher messagePublisher)
        {
            this.shippingOrderRepository = shippingOrderRepository;
            this.aggregateRoot = aggregateRoot;
            this.messagePublisher = messagePublisher;
        }

        public async Task<ShippingOrder> Handle(CreateShippingOrderCommand request, CancellationToken cancellationToken)
        {
            var streamId = Guid.NewGuid();

            var eventVersion = 0;
            var @event = new ShippingOrderCreated(
                streamId,
                OrderStatus.Pending,
                request.Address,
                request.Description,
                request.PlacedBy,
                eventVersion);

            await aggregateRoot.Apply(new List<IEvent> { @event });
            
            await shippingOrderRepository.AddEventToStreamAsync(streamId, @event, cancellationToken);

            await messagePublisher.PublishAsync(streamId, @event, cancellationToken);

            return aggregateRoot.CurrentState;
        }
    }
}
