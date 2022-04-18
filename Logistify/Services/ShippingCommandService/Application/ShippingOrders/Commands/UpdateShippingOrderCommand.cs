using Application.Abstractions;
using Application.EventSourcing.EsFramework;
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

        public UpdateShippingOrderCommandHandler(
            IShippingOrderRepository shippingOrderRepository,
            IAggregateRoot<ShippingOrder> aggregateRoot)
        {
            this.shippingOrderRepository = shippingOrderRepository;
            this.aggregateRoot = aggregateRoot;
        }

        public async Task<ShippingOrder> Handle(UpdateShippingOrderCommand request, CancellationToken cancellationToken)
        {
            var previousEvents = await shippingOrderRepository
                .GetEventsByAggregateIdAsync(request.Id, cancellationToken);

            if (previousEvents is null || !previousEvents.Any())
            {
                return null;
            }

            var @event = new ShippingOrderUpdated(request.Address, request.Description);

            await shippingOrderRepository.AddEventToStreamAsync(request.Id, @event, cancellationToken);
            
            previousEvents.Add(@event);
            await aggregateRoot.Apply(previousEvents);

            return aggregateRoot.CurrentState;
        }
    }
}
