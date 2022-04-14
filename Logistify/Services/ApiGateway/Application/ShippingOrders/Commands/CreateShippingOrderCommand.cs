using Application.Interfaces;
using Application.Models;
using MediatR;

namespace Application.ShippingOrders.Commands
{
    public class CreateShippingOrderCommand : IRequest<ShippingOrderDetailsDto>
    {
        public CreateShippingOrderCommand(string description, string placedBy, string address)
        {
            Description = description;
            PlacedBy = placedBy;
            Address = address;
        }

        public string Description { get; }
        public string PlacedBy { get; }
        public string Address { get; set; }
    }

    public class CreateShippingOrderCommandHandler : IRequestHandler<CreateShippingOrderCommand, ShippingOrderDetailsDto>
    {
        private readonly IShippingOrderCommandService orderCommandService;

        public CreateShippingOrderCommandHandler(IShippingOrderCommandService orderCommandService)
        {
            this.orderCommandService = orderCommandService;
        }

        public Task<ShippingOrderDetailsDto> Handle(CreateShippingOrderCommand request, CancellationToken cancellationToken)
        {
            var orderDetails = new ShippingOrderDetailsDto
            {
                PlacedBy = request.PlacedBy,
                Address = request.Address,
                Description = request.Description,
            };

            return orderCommandService.CreateShippingOrderAsync(orderDetails, cancellationToken);
        }
    }
}
