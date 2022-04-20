using Application.Interfaces;
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
        private readonly IShippingOrderCommandClient shippingOrderCommand;

        public CancelShippingOrderCommandHandler(IShippingOrderCommandClient shippingOrderCommand)
        {
            this.shippingOrderCommand = shippingOrderCommand;
        }

        public Task<bool> Handle(CancelShippingOrderCommand request, CancellationToken cancellationToken)
        {
            return shippingOrderCommand.CancelShippingOrderAsync(request.OrderId, cancellationToken);
        }
    }
}
