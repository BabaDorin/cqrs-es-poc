using Domain.Entities;
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
        public Task<ShippingOrder> Handle(CreateShippingOrderCommand request, CancellationToken cancellationToken)
        {
            // To be delivered (Event Sourcing)
            throw new NotImplementedException();
        }
    }
}
