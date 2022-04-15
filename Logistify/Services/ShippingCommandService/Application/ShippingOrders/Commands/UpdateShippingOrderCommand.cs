using Domain.Entities;
using MediatR;

namespace Application.ShippingOrders.Commands
{
    public class UpdateShippingOrderCommand : IRequest<ShippingOrder>
    {
        public UpdateShippingOrderCommand(string address, string description)
        {
            Address = address;
            Description = description;
        }

        public string Address { get; }
        public string Description { get; }
    }

    public class UpdateShippingOrderCommandHandler : IRequestHandler<UpdateShippingOrderCommand, ShippingOrder>
    {
        public Task<ShippingOrder> Handle(UpdateShippingOrderCommand request, CancellationToken cancellationToken)
        {
            // To be delivered (Event Sourcing)
            throw new NotImplementedException();
        }
    }
}
