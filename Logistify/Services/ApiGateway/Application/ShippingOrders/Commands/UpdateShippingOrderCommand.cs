using Application.Interfaces;
using Application.Models;
using MediatR;

namespace Application.ShippingOrders.Commands
{
    public class UpdateShippingOrderCommand : IRequest<ShippingOrderDetailsDto>
    {
        public UpdateShippingOrderCommand(Guid id, string address, string description, string updatedBy)
        {
            Id = id;
            Address = address;
            Description = description;
            UpdatedBy = updatedBy;
        }

        public Guid Id { get; }
        public string Address { get; }
        public string Description { get; }
        public string UpdatedBy { get; }
    }

    public class UpdateShippingOrderCommandHandler : IRequestHandler<UpdateShippingOrderCommand, ShippingOrderDetailsDto>
    {
        private readonly IShippingOrderCommandClient orderCommandService;

        public UpdateShippingOrderCommandHandler(IShippingOrderCommandClient orderCommandService)
        {
            this.orderCommandService = orderCommandService;
        }

        public Task<ShippingOrderDetailsDto> Handle(UpdateShippingOrderCommand request, CancellationToken cancellationToken)
        {
            var orderDetails = new ShippingOrderDetailsDto
            {
                Address = request.Address,
                Description = request.Description,
            };

            return orderCommandService.UpdateShippingOrderAsync(request.Id, orderDetails, cancellationToken);
        }
    }
}
