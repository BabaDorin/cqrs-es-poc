using Application.Interfaces;
using Application.Models;
using ShippingCommandGrpc.Presentation;

namespace Infrastructure.Services
{
    public class ShippingCommandServiceClient : IShippingOrderCommandClient
    {
        private readonly ShippingCommandService.ShippingCommandServiceClient client;

        public ShippingCommandServiceClient(ShippingCommandService.ShippingCommandServiceClient client)
        {
            this.client = client;
        }

        public async Task<ShippingOrderDetailsDto> CreateShippingOrderAsync(ShippingOrderDetailsDto orderDetails, CancellationToken cancellationToken)
        {
            var request = new CreateShippingOrderRequest()
            {
                Address = orderDetails.Address,
                Description = orderDetails.Description,
                PlacedBy = orderDetails.PlacedBy
            };

            var result = await client.CreateShippingOrderAsync(request, cancellationToken: cancellationToken);

            var dto = new ShippingOrderDetailsDto
            {
                Id = Guid.Parse(result.Id),
                Address = result.Address,
                Description = result.Description,
                PlacedBy = result.PlacedBy,
                Status = (Domain.Enums.OrderStatus)result.Status
            };

            return dto;
        }

        public async Task<ShippingOrderDetailsDto> UpdateShippingOrderAsync(Guid id, ShippingOrderDetailsDto orderDetails, CancellationToken cancellationToken)
        {
            var request = new UpdateShippingOrderRequest
            {
                Id = id.ToString(),
                Address = orderDetails.Address,
                Description = orderDetails.Description
            };
            
            var result = await client.UpdateShippingOrderAsync(request, cancellationToken: cancellationToken);

            var dto = new ShippingOrderDetailsDto
            {
                Id = Guid.Parse(result.Id),
                Address = result.Address,
                Description = result.Description,
                PlacedBy = result.PlacedBy,
                Status = (Domain.Enums.OrderStatus)result.Status
            };

            return dto;
        }
    }
}
