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
        public Task<bool> Handle(CancelShippingOrderCommand request, CancellationToken cancellationToken)
        {
            // To be delivered (Event Sourcing)
            throw new NotImplementedException();
        }
    }
}
