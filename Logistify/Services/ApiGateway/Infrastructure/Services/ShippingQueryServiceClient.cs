using Application.Interfaces;
using Application.Models;
using ShippingQueryGrpc.Presentation;

namespace Infrastructure.Services
{
    public class ShippingQueryServiceClient : IShippingOrderQueryClient
    {
        private readonly ShippingQueryService.ShippingQueryServiceClient client;

        public ShippingQueryServiceClient(ShippingQueryService.ShippingQueryServiceClient client)
        {
            this.client = client;
        }

        public async Task<IList<ShippingOrderDto>> GetAsync(CancellationToken cancellationToken)
        {
            var result = await client.GetShippingOrdersAsync(new Google.Protobuf.WellKnownTypes.Empty(), cancellationToken: cancellationToken);

            return result.Orders != null && result.Orders.Any()
                ? result.Orders.Select(x => new ShippingOrderDto
                {
                    Id = Guid.Parse(x.Id),
                    Address = x.Address,
                    Status = (Domain.Enums.OrderStatus)x.Status,
                }).ToList()
                : new List<ShippingOrderDto>();
        }

        public async Task<ShippingOrderDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var request = new GetShippingOrderByIdRequest
            {
                Id = id.ToString()
            };

            var result = await client.GetShippingOrderByIdAsync(request, cancellationToken: cancellationToken);

            if (result is null) return null;

            return new ShippingOrderDetailsDto
            {
                Id = Guid.Parse(result.Id),
                Address = result.Address,
                Description = result.Description,
                PlacedBy = result.PlacedBy,
                Status = (Domain.Enums.OrderStatus)result.Status,
            };
        }
    }
}
