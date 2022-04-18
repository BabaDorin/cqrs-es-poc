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

        public CreateShippingOrderCommandHandler(
            IShippingOrderRepository shippingOrderRepository,
            IAggregateRoot<ShippingOrder> aggregateRoot)
        {
            this.shippingOrderRepository = shippingOrderRepository;
            this.aggregateRoot = aggregateRoot;
        }

        public async Task<ShippingOrder> Handle(CreateShippingOrderCommand request, CancellationToken cancellationToken)
        {
            var streamId = Guid.NewGuid();

            var @event = new ShippingOrderCreated(
                streamId,
                OrderStatus.Pending,
                request.Address,
                request.Description,
                request.PlacedBy);

            await shippingOrderRepository.AddEventToStreamAsync(streamId, @event, cancellationToken);

            await aggregateRoot.Apply(new List<IEvent> { @event });

            return aggregateRoot.CurrentState;
        }
    }
}
